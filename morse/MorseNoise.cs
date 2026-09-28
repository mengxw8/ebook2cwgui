using System;

namespace CW.morse
{
    /// <summary>
    /// ebook2cw 默认 500 Hz 档：CW 增益、白噪声、六阶带通。
    /// 用双线性变换将 11025 Hz 的原始滤波器转换到输出采样率。
    /// SNR 沿用原版近似刻度；通带中心对准 CW 音调，避免载波落在阻带。
    /// </summary>
    internal sealed class MorseNoise
    {
        // 原程序默认采样率；固定系数不能直接用于 44100 Hz，否则通带会偏移。
        private const double ReferenceSampleRate = 11025;
        // 来自 ebook2cw.c 的 snramplitude；索引 snr + 10 对应 -10..+10 dB。
        private static readonly double[] SnrGain =
        {
            0.042, 0.0475, 0.0533, 0.0599, 0.067, 0.075, 0.084,
            0.094, 0.1055, 0.1187, 0.134, 0.15, 0.168, 0.19,
            0.213, 0.238, 0.267, 0.3, 0.335, 0.378, 0.425
        };
        // 原版 filterloop 的分母，按 z^-1 的升幂排列（反馈系数取反）。
        private static readonly double[] ReferenceDenominator =
        {
            1, -4.3051177389, 8.4472239809, -9.5144072211,
            6.4847845669, -2.5370880100, 0.4535459334
        };
        private readonly Random random = new();
        private readonly double signalGain;
        private readonly double noiseAmplitude;
        private readonly double[] denominator = new double[7];
        // 每个实例独占滤波历史；跨音频块保留状态，不可由播放和导出共用。
        // 处理当前样本时，索引 i 表示延迟 i 个样本的输入/输出。
        private readonly double[] x = new double[7];
        private readonly double[] y = new double[7];
        private readonly double numeratorGain;

        /// <param name="snr">原版信噪比刻度，范围 -10..10 dB，并非精密功率标定。</param>
        /// <param name="sampleRate">单声道样本的采样率（Hz），至少 11025。</param>
        /// <param name="frequency">期望的通带中心（Hz），通常取 CW 音调，必须低于奈奎斯特频率。</param>
        public MorseNoise(int snr, int sampleRate, double frequency)
        {
            if (snr < -10 || snr > 10)
                throw new ArgumentOutOfRangeException(nameof(snr));
            if (sampleRate < ReferenceSampleRate)
                throw new ArgumentOutOfRangeException(nameof(sampleRate));

            if (!double.IsFinite(frequency) || frequency <= 0 || frequency >= sampleRate / 2.0)
                throw new ArgumentOutOfRangeException(nameof(frequency));

            // 原版增益表在 +10 dB 只有 0.425，再换算到 20000 峰值后，电码只有干净信号的约四分之一。
            // 以 +10 dB 为满幅基准，较低档仍按原表比例下降，信噪比刻度不变。
            const double maxSnrGain = 0.425;
            const double loudness = 0.82;
            signalGain = SnrGain[snr + 10] / maxSnrGain * loudness;
            // 1062 Hz 是原版 500 Hz 档系数的近似通带中心，并非其配置中的 800 Hz。
            // tan 预畸变同时考虑采样率和中心频率，使所选 CW 音调落在通带中。
            // 中心移动后带宽也随之变化，不保证仍是严格的 500 Hz 带宽。
            double rateRatio = Math.Tan(Math.PI * 1062 / ReferenceSampleRate)
                / Math.Tan(Math.PI * frequency / sampleRate);
            // 原版白噪声峰值为 10000；按频率缩放比的平方根近似补偿带内噪声功率。
            // 信号抬到接近满幅后，噪声乘同一倍率，避免只把电码变响而改变信噪比。
            double levelCompensation = loudness * short.MaxValue / (maxSnrGain * 20000.0);
            noiseAmplitude = 10000 * Math.Sqrt(rateRatio) * levelCompensation;

            // z_old^-1 = (q + z_new^-1) / (1 + q*z_new^-1)。
            // 同时转换六个极点与零点，保留原版 Butterworth 形状。
            double q = (1 - rateRatio) / (1 + rateRatio);
            for (int k = 0; k <= 6; k++)
            {
                // 分母第 k 项转换后为 a[k]*(q+t)^k*(1+q*t)^(6-k)，t=z_new^-1。
                // 用一阶多项式逐次相乘，避免手工展开六阶系数。
                double[] polynomial = { 1 };
                for (int j = 0; j < 6; j++)
                {
                    double constant = j < k ? q : 1;
                    double linear = j < k ? 1 : q;
                    var next = new double[polynomial.Length + 1];
                    for (int i = 0; i < polynomial.Length; i++)
                    {
                        next[i] += polynomial[i] * constant;
                        next[i + 1] += polynomial[i] * linear;
                    }
                    polynomial = next;
                }
                for (int i = 0; i <= 6; i++)
                    denominator[i] += ReferenceDenominator[k] * polynomial[i];
            }
            // 归一化分母首项为 1，供下方 IIR 差分方程使用。
            // 原版分子为 (1-z^-2)^3 / 188.6640723；变换后多出 (1-q²)^3。
            double normalization = denominator[0];
            numeratorGain = Math.Pow(1 - q * q, 3) / (188.6640723 * normalization);
            for (int i = 0; i <= 6; i++) denominator[i] /= normalization;
        }

        /// <summary>顺序处理一个单声道样本；该方法会推进随机序列和滤波状态，不可并发调用。</summary>
        /// <param name="sample">干净的 16 位 PCM 样本；码元间隔传 0，仍会生成底噪。</param>
        /// <param name="volume">混合、滤波后的总音量，同时作用于电码和噪声。</param>
        public short Process(short sample, float volume = 1)
        {
            // 与原版相同，信号和噪声先混合，再一起经过带通。
            // 用 double 保存中间结果，避免原版 short 转换和 float 反馈的精度损失。
            double input = sample * signalGain + (random.NextDouble() * 2 - 1) * noiseAmplitude;
            for (int i = 6; i > 0; i--)
            {
                x[i] = x[i - 1];
                y[i] = y[i - 1];
            }
            x[0] = input;
            // 分子展开为 1 - 3z^-2 + 3z^-4 - z^-6，随后减去分母反馈项。
            double output = numeratorGain * (x[0] - 3 * x[2] + 3 * x[4] - x[6]);
            for (int i = 1; i <= 6; i++) output -= denominator[i] * y[i];
            y[0] = output;
            // 噪声尖峰可能超过满幅。线性区保持原样，超出部分软限幅，避免硬削波翻转。
            return (short)Math.Clamp(SoftLimit(output) * volume, short.MinValue, short.MaxValue);
        }

        private static double SoftLimit(double sample)
        {
            const double ceiling = short.MaxValue;
            const double knee = 0.75;
            double x = sample / ceiling;
            double ax = Math.Abs(x);
            if (ax <= knee)
                return sample;
            double excess = (ax - knee) / (1 - knee);
            double shaped = knee + (1 - knee) * Math.Tanh(excess);
            return Math.CopySign(ceiling * shaped, sample);
        }
    }
}

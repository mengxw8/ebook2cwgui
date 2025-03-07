using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW
{
    internal class SineWaveProvider : WaveProvider16
    {
        private readonly int sampleRate;
        private readonly double frequency;



        public bool Mute { get; set; }

        // 构造函数
        public SineWaveProvider(double frequency, int sampleRate= 44100, int channels =1) : base(sampleRate, 1)
        {
            if (channels < 1 || channels > 2)
                throw new ArgumentException("Channels must be 1 (mono) or 2 (stereo).");

            this.sampleRate = sampleRate;
            this.frequency = frequency;

        }


        // 读取音频数据
        /// <summary>
        /// 实现 IWaveProvider 的 Read 方法
        /// </summary>
        public override int Read(short[] buffer, int offset, int count)
        {
            int samplesRead = 0;
            for (int i = 0; i < count; i++) {
                if (Mute)
                {
                    // 如果队列为空，填充静音
                    buffer[offset + samplesRead] = 0;
                    samplesRead++;

                }
                else
                {
                    double phase = 2 * Math.PI * frequency * i / sampleRate;
                    double sample = Math.Sin(phase);
                    // 直接转换为 short 并限制范围
                    buffer[offset + samplesRead] = (short)(sample * short.MaxValue);
                    samplesRead++;
                }
            }

            return samplesRead * sizeof(short);
        }
    }
}

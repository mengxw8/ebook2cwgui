using NAudio.Wave;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CW
{
    internal class MorsePlayer : WaveProvider16
    {
        private readonly ConcurrentQueue<short> audioQueue = new();
        private readonly ConcurrentQueue<string> charQueue = new();
        private readonly int sampleRate;
        private int frequency;
        public float Volume { get; set; }


        private MorseConfig config;
        //上升沿的采样数
        private int riseTime = 50;
        //下降沿的采样数
        private int fallTime = 50;
        // 单位时间 T 的样本数
        private int dotDuration;
        //点划对应关系
        private Dictionary<char, string>? keys;
        //文件迭代器
        private IEnumerable<string>? strings;

        //把计算好的结果缓存起来，不用重复计算
        public short[]? Dit_buff { get; set; }
        public short[]? Dah_buff { get; set; }
        private readonly bool InfiniteLength;

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="sampleRate">采样率</param>
        /// <param name="frequency">频率</param>
        /// <param name="amplitude">音量</param>
        /// <param name="config">速度配置</param>
        public MorsePlayer(int frequency, MorseConfig config, int sampleRate = 44100, float amplitude = 0.8f, bool infiniteLength = true) : base(sampleRate, 2)
        {
            this.sampleRate = sampleRate;
            this.frequency = frequency;
            this.Volume = amplitude;
            this.config = config;
            this.InfiniteLength = infiniteLength;
            UpdateConfig(config);
            //this.waveFormat = new WaveFormat(sampleRate, 16, 2);  // 双声道格式
        }


        /// <summary>
        /// 更新配置并重新计算时间单位
        /// </summary>
        public void UpdateConfig(MorseConfig newConfig)
        {
            this.config = newConfig;
            this.dotDuration = (sampleRate * config.Di) / 1000; // 转换为样本数
            this.riseTime = Math.Min(50, dotDuration / 2);
            this.fallTime = Math.Min(50, dotDuration / 2);

            //this.dotDuration -= riseTimeDuration;
            //this.dotDuration -= fallTimeDuration;
            //计算波形
            Dit_buff = new short[dotDuration];
            Dah_buff = new short[dotDuration * 3];


            GenerateDitBuffer();
            GenerateDahBuffer();

        }
        private void GenerateDitBuffer()
        {
            Dit_buff = new short[dotDuration];
            for (int i = 0; i < dotDuration; i++)
            {
                double phase = 2 * Math.PI * frequency * i / sampleRate;
                double sample = Math.Sin(phase);

                // 淡入处理
                if (i < riseTime)
                {
                    double t = i / (double)riseTime;
                    sample *= Math.Pow(Math.Sin(t * Math.PI / 2), 2);
                }

                // 淡出处理
                if (i >= dotDuration - fallTime)
                {
                    int fallIndex = i - (dotDuration - fallTime);
                    double t = fallIndex / (double)(fallTime - 1);
                    sample *= Math.Pow(Math.Cos(t * Math.PI / 2), 2);
                }

                Dit_buff[i] = (short)(sample * short.MaxValue);
            }
        }

        private void GenerateDahBuffer()
        {
            int dahDuration = 3 * dotDuration;
            Dah_buff = new short[dahDuration];
            for (int i = 0; i < dahDuration; i++)
            {
                double phase = 2 * Math.PI * frequency * i / sampleRate;
                double sample = Math.Sin(phase);

                // 淡入处理
                if (i < riseTime)
                {
                    double t = i / (double)riseTime;
                    sample *= Math.Pow(Math.Sin(t * Math.PI / 2), 2);
                }

                // 淡出处理
                if (i >= dahDuration - fallTime)
                {
                    int fallIndex = i - (dahDuration - fallTime);
                    double t = fallIndex / (double)(fallTime - 1);
                    sample *= Math.Pow(Math.Cos(t * Math.PI / 2), 2);
                }

                Dah_buff[i] = (short)(sample * short.MaxValue);
            }
        }


        public void UpdateFrequency(int frequency)
        {
            this.frequency = frequency;
            UpdateConfig(config);

        }
        public void UpdateEncoding(Dictionary<char, string> encoding)
        {
            if (encoding != null) {
                this.keys = encoding;
            }
            


        }
        public void Clean()
        {
            charQueue.Clear();
            audioQueue.Clear();
        }
        public void AddMorseCode(string morseCode, Dictionary<char, string> keys)
        {
            this.keys = keys;
            AddMorseCode(morseCode, keys, config.Speed);
        }

        /// <summary>
        /// 添加莫尔斯电码到播放队列
        /// </summary>
        public void AddMorseCode(string morseCode, Dictionary<char, string> keys, int speed)
        {
            config = MorseConfig.Create(speed);
            UpdateConfig(config);
            this.keys = keys;
            morseCode= morseCode.Replace("\r\n"," ").ToUpper();

            //分割成每一组
            string[] chars = morseCode.Split(' ');
            foreach (string c in chars)
            {
                charQueue.Enqueue(c);
            }
        }
        private void ParseMusic()
        {
            var flag = charQueue.TryDequeue(out string? ch);
            if (flag && ch != null)
            {

                //每个字母
                foreach (char c in ch)
                {
                    //每个莫尔斯
                    if (!keys!.ContainsKey(c))
                    {
                        continue;
                    }
                    var code = keys[c];
                    foreach (char m in code)
                    {
                        switch (m)
                        {
                            case '.': EnqueueTone(Dit_buff!); break;
                            case '-': EnqueueTone(Dah_buff!); break;
                        }
                        EnqueueSilence(dotDuration); // 符号间隔1T
                    }

                    EnqueueSilence(3 * dotDuration); // 字符间隔3T
                }
                EnqueueSilence(4 * dotDuration); // 单词间隔补足到7T
            }
        }

        /// <summary>
        /// 将音调信号加入队列
        /// </summary>
        private void EnqueueTone(short[] duration)
        {
            foreach (var a in duration)
            {
                audioQueue.Enqueue(a);
            }
        }

        /// <summary>
        /// 将静音信号加入队列
        /// </summary>
        private void EnqueueSilence(int duration)
        {
            for (int i = 0; i < duration; i++)
            {
                audioQueue.Enqueue(0); // 静音
            }
        }

        /// <summary>
        /// 实现 IWaveProvider 的 Read 方法
        /// </summary>
        //public override int Read(short[] buffer, int offset, int count)
        //{
        //    Task? task = null;
        //    if (audioQueue.Count < count * sizeof(short)) {
        //       task= Task.Run(() => ParseMusic());
        //    }

        //    int samplesRead = 0;
        //    while (samplesRead < count) // 每个 float 样本占 4 字节
        //    {
        //        if (audioQueue.Count > 0)
        //        {
        //            audioQueue.TryDequeue(out short sample);
        //            sample = (short)(Volume * sample);
        //            // 直接转换为 short 并限制范围
        //            buffer[offset + samplesRead] = sample;
        //            samplesRead++;
        //        }
        //        else
        //        {
        //            // 如果队列为空，填充静音
        //            buffer[offset + samplesRead] = 0;
        //            samplesRead++;
        //        }
        //    }
        //        task?.Wait();


        //    return samplesRead * sizeof(short);
        //}
        public override int Read(short[] buffer, int offset, int count)
        {
            int samplesRead = 0;
            while (samplesRead < count)
            {
                if (audioQueue.Count < count)
                {
                    ParseMusic();
                }
                if (audioQueue.TryDequeue(out short sample))
                {
                    sample = (short)(Volume * sample);
                    // 立体声复制到左右声道
                    buffer[offset + samplesRead++] = sample;
                }
                else
                {

                    if (!InfiniteLength)
                    {
                        return 0;
                    }
                    // 填充静音（双声道）
                    buffer[offset + samplesRead++] = 0;
                }
                samplesRead++;
            }
            return count;
        }
    }
}




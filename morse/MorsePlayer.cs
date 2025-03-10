using NAudio.Wave;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW
{
    internal class MorsePlayer : WaveProvider16
    {
        private readonly ConcurrentQueue<short> audioQueue = new ConcurrentQueue<short>();
        private readonly int sampleRate;
        private int frequency;
        public float Volume{ get; set; }


        private MorseConfig config;
        //上升沿的采样数
        private int riseTime = 50;
        //下降沿的采样数
        private int fallTime = 50;
        // 单位时间 T 的样本数
        private int dotDuration;

        //把计算好的结果缓存起来，不用重复计算
        public short[] dit_buff { get; set; }
        public short[] dah_buff { get; set; }
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="sampleRate">采样率</param>
        /// <param name="frequency">频率</param>
        /// <param name="amplitude">音量</param>
        /// <param name="config">速度配置</param>
        public MorsePlayer(int frequency, MorseConfig config, int sampleRate = 44100, float amplitude = 0.8f):base(sampleRate,1)
        {
            this.sampleRate = sampleRate;
            this.frequency = frequency;
            this.Volume = amplitude;
            UpdateConfig(config);
            //this.waveFormat = new WaveFormat(sampleRate, 16, 2);  // 双声道格式
        }


        /// <summary>
        /// 更新配置并重新计算时间单位
        /// </summary>
        public void UpdateConfig(MorseConfig newConfig)
        {
            this.config = newConfig;
            int wpm = Math.Max(1, config.Speed); // 确保 WPM 不为 0

            this.dotDuration = (50* sampleRate) /(60 * wpm) ; // 转换为样本数
            this.riseTime = Math.Min(50, dotDuration / 2);
            this.fallTime = Math.Min(50, dotDuration / 2);

            //this.dotDuration -= riseTimeDuration;
            //this.dotDuration -= fallTimeDuration;
            //计算波形
            dit_buff = new short[dotDuration];
            dah_buff = new short[dotDuration*3];


            GenerateDitBuffer();
            GenerateDahBuffer();

        }
        private void GenerateDitBuffer()
        {
            dit_buff = new short[dotDuration];
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

                dit_buff[i] = (short)(sample * short.MaxValue );
            }
        }

        private void GenerateDahBuffer()
        {
            int dahDuration = 3 * dotDuration;
            dah_buff = new short[dahDuration];
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

                dah_buff[i] = (short)(sample * short.MaxValue );
            }
        }


        public void UpdateFrequency(int frequency)
        {
            this.frequency = frequency;
            UpdateConfig(config);

        }
        public void Clean() {
            audioQueue.Clear();
        }
        public void AddMorseCode(string morseCode, Dictionary<char, string> keys) {
            AddMorseCode( morseCode,  keys, config.Speed);
        }
        /// <summary>
        /// 添加莫尔斯电码到播放队列
        /// </summary>
        public void AddMorseCode(string morseCode, Dictionary<char, string> keys,int speed)
        {
            config.Speed = speed;
            UpdateConfig(config);

            //分割成每一组
            string[] chars = morseCode.Split(' ');
            foreach (var ch in chars)
            {
                //每个字母
                foreach (char c in ch)
                {
                    //每个莫尔斯
                    if (!keys.ContainsKey(c))
                    {
                        continue;
                    }
                  var  code= keys[c];
                    foreach (char m in code)
                    {
                        switch (m)
                        {
                            case '.': EnqueueTone(dit_buff); break;
                            case '-': EnqueueTone(dah_buff); break;
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
            for (int i = 0; i < duration ; i++)
            {
                audioQueue.Enqueue(0); // 静音
            }
        }

        /// <summary>
        /// 实现 IWaveProvider 的 Read 方法
        /// </summary>
        public override int Read(short[] buffer, int offset, int count)
        {
            int samplesRead = 0;
            while (samplesRead < count ) // 每个 float 样本占 4 字节
            {
                if (audioQueue.Count > 0)
                {
                    audioQueue.TryDequeue(out short sample);
                        sample =(short) (Volume* sample);
                    // 直接转换为 short 并限制范围
                    buffer[offset + samplesRead] = sample;
                    samplesRead++;
                }
                else
                {
                    // 如果队列为空，填充静音
                    buffer[offset + samplesRead] = 0;
                    samplesRead++;
                }
            }
            return samplesRead * sizeof(short);
        }
    }
}

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
        private  float amplitude;

        private MorseConfig config;
        //上升沿的采样数
        private int riseTime = 50;
        //下降沿的采样数
        private int fallTime = 50;
        // 单位时间 T 的样本数
        private int dotDuration;

        //把计算好的结果缓存起来，不用重复计算
        private short[] dit_buff;
        private short[] dah_buff;
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="sampleRate">采样率</param>
        /// <param name="frequency">频率</param>
        /// <param name="amplitude">音量</param>
        /// <param name="config">速度配置</param>
        public MorsePlayer(int frequency, MorseConfig config, int sampleRate = 44100, float amplitude = 0.9f):base(sampleRate,1)
        {
            this.sampleRate = sampleRate;
            this.frequency = frequency;
            this.amplitude = amplitude;
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
            int tInSeconds = 1200 / wpm;       // 单位时间 T（秒）


            this.dotDuration = (tInSeconds * sampleRate) / 1000; // 转换为样本数
            //this.dotDuration -= riseTimeDuration;
            //this.dotDuration -= fallTimeDuration;
            //计算波形
            dit_buff = new short[dotDuration];
            dah_buff = new short[dotDuration*3];

            for (int i = 0; i < dotDuration * 3; i++){
               

                var sample =(Math.Sin(2 * Math.PI * frequency * i / sampleRate));

               
                if (i < riseTime) {
                    sample *=  Math.Pow(Math.Sin(i * Math.PI / (2.0 * riseTime)), 2);
                  
                }
                var dit = sample;
                var dah = sample;
                if (i < dotDuration) {
                    if (i >( dotDuration - fallTime)) {
                        dit *= Math.Pow(Math.Sin(2* Math.PI*(i-(dotDuration-fallTime)+fallTime / (4 * fallTime))), 2);
                    }

                    dit_buff[i] = (short)(dit * short.MaxValue* amplitude);
                }
                if (i > (3 * dotDuration - fallTime)) {
                    dah*= Math.Pow(Math.Sin(2  * Math.PI * (i - (3*dotDuration - fallTime) + fallTime / (4 * fallTime))), 2);
                }
                dah_buff[i] = (short)(dah * short.MaxValue* amplitude);
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

        /// <summary>
        /// 添加莫尔斯电码到播放队列
        /// </summary>
        public void AddMorseCode(string morseCode, Dictionary<char, string> keys)
        {

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
        private void EnqueueSilence(float duration)
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

using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW
{
    internal class SineWaveProvider : IWaveProvider
    {
        private readonly int _sampleRate;
        private readonly int _channels;
        private readonly double _frequency;
        private readonly float _amplitude;
        private long _currentSample;

        // 构造函数
        public SineWaveProvider(double frequency, int sampleRate=44100, int channels =1,  float amplitude = 0.7f)
        {
            if (channels < 1 || channels > 2)
                throw new ArgumentException("Channels must be 1 (mono) or 2 (stereo).");

            _sampleRate = sampleRate;
            _channels = channels;
            _frequency = frequency;
            _amplitude = Math.Clamp(amplitude, 0.0f, 1.0f);

            // 设置音频格式为16位PCM
            WaveFormat = new WaveFormat(_sampleRate, 16, _channels);
        }

        // 音频格式属性
        public WaveFormat WaveFormat { get; }

        // 读取音频数据
        public int Read(byte[] buffer, int offset, int count)
        {
            int bytesPerSample = 2; // 16-bit PCM
            int samplesRequired = count / (bytesPerSample * _channels);
            int samplesRead = 0;

            for (int n = 0; n < samplesRequired; n++)
            {
                // 计算正弦波值并应用幅度
                double time = (double)_currentSample / _sampleRate;
                float sample = (float)(Math.Sin(2 * Math.PI * _frequency * time)) * _amplitude;

                // 将浮点样本缩放到16位整数范围
                short sample16 = (short)(sample * short.MaxValue);

                // 写入缓冲区，处理多通道
                for (int channel = 0; channel < _channels; channel++)
                {
                    Buffer.BlockCopy(BitConverter.GetBytes(sample16), 0, buffer, offset + (n * bytesPerSample * _channels) + (channel * bytesPerSample), bytesPerSample);
                }

                _currentSample++;
                samplesRead++;
            }

            return samplesRead * bytesPerSample * _channels;
        }
    }
}

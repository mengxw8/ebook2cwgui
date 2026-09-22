using NAudio.Wave;
using System;
using System.Threading;

namespace CW
{
    /// <summary>
    /// 一路报文。文本和编码在 Prepare/重播时冻结；音量、频率、波形、噪声和静音可在播放中更改。
    /// </summary>
    internal sealed class ChannelVoice
    {
        private readonly MorsePlayer player;
        private string playingText = "";
        private Dictionary<char, string> playingKeys = new();
        private int speed = 20;
        private string waveform = "正弦波";
        private bool noiseEnabled;
        private int noiseSnr;
        private volatile bool isPrimary;
        private volatile bool loop;
        private volatile bool muted;
        private volatile bool finished;

        public ChannelVoice(int frequency, int speed, string waveform, int sampleRate = 44100)
        {
            this.speed = speed;
            this.waveform = waveform;
            player = new MorsePlayer(frequency, MorseConfig.Create(speed, waveform), sampleRate, 0.5f, infiniteLength: false);
            player.OnGroupPlay += group => CurrentGroup = group;
        }

        public bool IsPrimary
        {
            get => isPrimary;
            set => isPrimary = value;
        }

        public bool Loop
        {
            get => loop;
            set => loop = value;
        }

        public bool Muted
        {
            get => muted;
            set => muted = value;
        }

        public bool IsFinished => finished;

        public bool HasText => !string.IsNullOrWhiteSpace(playingText);

        public int RestartCount { get; private set; }

        public string CurrentGroup { get; private set; } = "";

        public void ClearDisplay() => CurrentGroup = "";

        public void Prepare(string text, Dictionary<char, string> keys, int speed, string waveform, int frequency, float volume, bool noiseEnabled, int noiseSnr)
        {
            playingText = text ?? "";
            playingKeys = new Dictionary<char, string>(keys);
            this.speed = speed;
            this.waveform = string.IsNullOrWhiteSpace(waveform) ? "正弦波" : waveform;
            this.noiseEnabled = noiseEnabled;
            this.noiseSnr = noiseSnr;
            RestartCount = 0;
            QueueFromStart(volume, frequency);
        }

        public void SetVolume(float volume) => player.Volume = volume;

        public void SetSpeed(int speed)
        {
            this.speed = speed;
            player.UpdateConfig(MorseConfig.Create(speed, waveform));
        }

        public void SetWaveform(string waveform)
        {
            this.waveform = string.IsNullOrWhiteSpace(waveform) ? "正弦波" : waveform;
            player.UpdateConfig(MorseConfig.Create(speed, this.waveform));
        }

        public void SetFrequency(int frequency)
        {
            player.UpdateFrequency(frequency);
            if (noiseEnabled)
                player.UpdateNoise(true, noiseSnr);
        }

        public void SetNoise(bool enabled, int snr)
        {
            noiseEnabled = enabled;
            noiseSnr = Math.Clamp(snr, -10, 10);
            player.UpdateNoise(enabled, noiseSnr);
        }

        /// <summary>音频线程调用。播完当前一遍返回 false，样本为 0。</summary>
        public bool Pull(out short sample)
        {
            if (finished)
            {
                sample = 0;
                return false;
            }

            if (!player.TryReadMonoSample(out sample))
            {
                finished = true;
                CurrentGroup = "";
                sample = 0;
                return false;
            }

            if (muted)
                sample = 0;
            return true;
        }

        /// <summary>音频线程调用。空文本保持结束状态，避免循环空报文时空转。</summary>
        public void Restart()
        {
            RestartCount++;
            QueueFromStart(player.Volume, 0, keepFrequency: true);
        }

        private void QueueFromStart(float volume, int frequency, bool keepFrequency = false)
        {
            player.Clean();
            player.Volume = volume;
            if (!keepFrequency)
                player.UpdateFrequency(frequency);
            player.UpdateConfig(MorseConfig.Create(speed, waveform));
            player.UpdateEncoding(playingKeys);
            if (!keepFrequency)
                player.UpdateNoise(noiseEnabled, Math.Clamp(noiseSnr, -10, 10));
            finished = false;
            CurrentGroup = "";
            if (string.IsNullOrWhiteSpace(playingText))
            {
                finished = true;
                return;
            }

            player.AddMorseCode(playingText, playingKeys, speed, waveform);
        }
    }

    /// <summary>
    /// 多路共享一个采样时钟。主路结束且不循环时整段停止；主路循环时，未循环的次路跟着重来，循环次路按自己的文本继续。
    /// </summary>
    internal sealed class MultiChannelMixer : WaveProvider16
    {
        private ChannelVoice[] voices = [];
        private bool sessionEnded;
        private int sessionEndPosted;
        private SynchronizationContext? uiContext;

        public MultiChannelMixer(int sampleRate = 44100) : base(sampleRate, 2)
        {
        }

        public Action? SessionEnded { get; set; }

        public void SetUiContext(SynchronizationContext? context) => uiContext = context;

        public void BeginSession(ChannelVoice[] voices)
        {
            this.voices = voices;
            sessionEnded = false;
            Interlocked.Exchange(ref sessionEndPosted, 0);
        }

        public override int Read(short[] buffer, int offset, int count)
        {
            if (sessionEnded || count < 2)
                return 0;

            var snapshot = voices;
            int frames = count / 2;
            int written = 0;
            for (int frame = 0; frame < frames; frame++)
            {
                int mixed = 0;
                bool primaryEnded = false;
                bool hasPrimary = false;
                foreach (var voice in snapshot)
                {
                    if (voice.IsPrimary)
                        hasPrimary = true;

                    short sample = 0;
                    if (!voice.Pull(out sample))
                    {
                        // 循环次路按自己的文本接着播，不跟主路的圈边界切断。
                        if (!voice.IsPrimary && voice.Loop && voice.HasText)
                        {
                            voice.Restart();
                            voice.Pull(out sample);
                        }
                        else if (voice.IsPrimary)
                        {
                            primaryEnded = true;
                        }
                    }

                    mixed += sample;
                }

                buffer[offset + written++] = (short)Math.Clamp(mixed, short.MinValue, short.MaxValue);
                buffer[offset + written++] = (short)Math.Clamp(mixed, short.MinValue, short.MaxValue);

                if (!hasPrimary || primaryEnded)
                {
                    var primary = FindPrimary(snapshot);
                    if (primary is { Loop: true, HasText: true })
                    {
                        primary.Restart();
                        foreach (var voice in snapshot)
                        {
                            if (!voice.IsPrimary && !voice.Loop)
                                voice.Restart();
                        }
                    }
                    else
                    {
                        sessionEnded = true;
                        NotifySessionEnded();
                        break;
                    }
                }
            }

            return written;
        }

        private static ChannelVoice? FindPrimary(ChannelVoice[] snapshot)
        {
            foreach (var voice in snapshot)
            {
                if (voice.IsPrimary)
                    return voice;
            }

            return null;
        }

        private void NotifySessionEnded()
        {
            if (Interlocked.Exchange(ref sessionEndPosted, 1) != 0)
                return;

            var callback = SessionEnded;
            if (callback == null)
                return;

            // 只投递到界面线程。在音频回调里直接 Stop 会和声卡驱动互相等待。
            uiContext?.Post(_ => callback(), null);
        }
    }
}

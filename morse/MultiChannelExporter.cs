using NAudio.Lame;
using NAudio.Wave;

namespace CW
{
    internal sealed class ChannelMixSource
    {
        public string Text { get; set; } = "";
        public Dictionary<char, string> Code { get; set; } = [];
        public int Speed { get; set; } = 20;
        public string Waveform { get; set; } = "正弦波";
        public int Frequency { get; set; } = 600;
        public float Volume { get; set; } = 0.5f;
        public bool NoiseEnabled { get; set; }
        public int NoiseSnr { get; set; }
        public bool IsPrimary { get; set; }
        public bool Loop { get; set; }
        public bool Muted { get; set; }
    }

    internal enum MixExportResult
    {
        Completed,
        Empty,
    }

    /// <summary>
    /// 用独立的声部和混合器把当前各路渲染成文件，不占用正在播放的声部。
    /// 长度固定为主路完整播放一遍；循环次路在这一遍内继续。
    /// </summary>
    internal static class MultiChannelExporter
    {
        private const int SampleRate = 44100;
        private const int MaxSeconds = 4 * 60 * 60;

        public static MixExportResult Export(IReadOnlyList<ChannelMixSource> channels, string path, IProgress<TimeSpan>? progress, CancellationToken cancellation)
        {
            if (channels.Count == 0)
                return MixExportResult.Empty;

            var voices = new ChannelVoice[channels.Count];
            for (int i = 0; i < channels.Count; i++)
            {
                var source = channels[i];
                var voice = new ChannelVoice(source.Frequency, source.Speed, source.Waveform, SampleRate);
                voice.IsPrimary = source.IsPrimary;
                voice.Loop = source.IsPrimary ? false : source.Loop;
                voice.Muted = source.IsPrimary ? false : source.Muted;
                voice.Prepare(
                    source.Text,
                    source.Code,
                    source.Speed,
                    source.Waveform,
                    source.Frequency,
                    source.Volume,
                    source.NoiseEnabled,
                    source.NoiseSnr);
                voices[i] = voice;
            }

            var mixer = new MultiChannelMixer(SampleRate);
            mixer.BeginSession(voices);

            bool failed = true;
            try
            {
                var format = new WaveFormat(SampleRate, 16, 1);
                using var writer = CreateWriter(path, format);
                long framesWritten = Render(mixer, writer, progress, cancellation);
                if (framesWritten == 0)
                    return MixExportResult.Empty;

                failed = false;
                progress?.Report(TimeSpan.FromSeconds(framesWritten / (double)SampleRate));
                return MixExportResult.Completed;
            }
            finally
            {
                if (failed)
                    TryDelete(path);
            }
        }

        private static Stream CreateWriter(string path, WaveFormat format)
        {
            if (path.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
                return new WaveFileWriter(path, format);
            return new LameMP3FileWriter(path, format, LAMEPreset.VBR_90);
        }

        private static long Render(MultiChannelMixer mixer, Stream writer, IProgress<TimeSpan>? progress, CancellationToken cancellation)
        {
            short[] stereo = new short[SampleRate * 2];
            byte[] mono = new byte[SampleRate * sizeof(short)];
            long framesWritten = 0;
            long lastReport = 0;
            long maxFrames = (long)SampleRate * MaxSeconds;
            while (framesWritten < maxFrames)
            {
                cancellation.ThrowIfCancellationRequested();
                int read = mixer.Read(stereo, 0, stereo.Length);
                if (read < 2)
                    break;

                int frames = read / 2;
                for (int i = 0; i < frames; i++)
                {
                    short sample = stereo[i * 2];
                    mono[i * 2] = (byte)sample;
                    mono[i * 2 + 1] = (byte)(sample >> 8);
                }

                writer.Write(mono, 0, frames * sizeof(short));
                framesWritten += frames;
                if (progress != null && framesWritten - lastReport >= SampleRate)
                {
                    lastReport = framesWritten;
                    progress.Report(TimeSpan.FromSeconds(framesWritten / (double)SampleRate));
                }
            }

            return framesWritten;
        }

        private static void TryDelete(string path)
        {
            try
            {
                if (File.Exists(path))
                    File.Delete(path);
            }
            catch (Exception)
            {
                // 文件仍被占用时留下残稿，下次导出可以覆盖。
            }
        }
    }
}

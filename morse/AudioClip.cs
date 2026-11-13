using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace CW;

internal static class AudioClip
{
    public static short[] Load(string path)
    {
        using var reader = new AudioFileReader(path);
        ISampleProvider source = reader;
        if (source.WaveFormat.Channels == 2)
            source = new StereoToMonoSampleProvider(source);
        if (source.WaveFormat.Channels != 1)
            throw new InvalidDataException("请选择单声道或立体声音频文件。");
        if (source.WaveFormat.SampleRate != 44100)
            source = new WdlResamplingSampleProvider(source, 44100);
        var samples = new List<short>();
        var buffer = new float[8192];
        int count;
        while ((count = source.Read(buffer, 0, buffer.Length)) > 0)
            for (int i = 0; i < count; i++)
                samples.Add((short)Math.Clamp(buffer[i] * 32767f, short.MinValue, short.MaxValue));
        if (samples.Count == 0)
            throw new InvalidDataException("这个音频文件没有可播放的内容。");
        return samples.ToArray();
    }
}

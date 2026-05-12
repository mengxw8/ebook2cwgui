using Microsoft.VisualBasic;
using NAudio.Lame;
using NAudio.MediaFoundation;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CW.morse
{
    internal class MorseToMp3
    {
        /// <summary>
        ///  生成mp3文件，字幕文件是一组一组显示
        /// </summary>
        /// <param name="content">要生成的文本内容</param>
        /// <param name="keys">文本和嘀嗒对应关系</param>
        /// <param name="config">速度</param>
        /// <param name="outPath">输出文件路径</param>
        /// <param name="dit_buff">嘀的波形数据</param>
        /// <param name="dah_buff">嗒的波形数据</param>
        public static void ToMp3(String content, Dictionary<char, string> keys, MorseConfig config, string outPath, short[] dit_buff, short[] dah_buff)
        {



            // 创建LameMP3FileWriter，设置比特率（如128kbps）
            using var writer = new LameMP3FileWriter(outPath, new WaveFormat(44100, 1), LAMEPreset.VBR_90);

            byte[] bytes = new byte[dit_buff.Length * sizeof(short)];
            byte[] di = new byte[dit_buff.Length * sizeof(short)];
            Buffer.BlockCopy(dit_buff, 0, di, 0, di.Length);
            byte[] da = new byte[dah_buff.Length * sizeof(short)];
            Buffer.BlockCopy(dah_buff, 0, da, 0, da.Length);
            //分割成每一组
            string[] chars = content.Replace("\r\n", " ").Split(' ');
            //时间码
            long startTime = 0;
            long endTime = 0;
            long lineNo = 1;
            using var srtWriter = new StreamWriter(outPath.Replace(".mp3", ".srt"));
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
                    foreach (char m in keys[c])
                    {
                        switch (m)
                        {
                            case '.': writer.Write(di, 0, di.Length); endTime += config.Di; break;
                            case '-': writer.Write(da, 0, da.Length); endTime += config.Da; break;
                        }
                        // 符号间隔1T
                        writer.Write(bytes, 0, bytes.Length);
                        endTime += config.Di;
                    }
                    // 字符间隔3T
                    writer.Write(bytes, 0, bytes.Length);
                    writer.Write(bytes, 0, bytes.Length);
                    writer.Write(bytes, 0, bytes.Length);
                    endTime += config.Di * 3;
                }
                TimeSpan startTimeSpan = TimeSpan.FromMilliseconds(startTime);
               int startTotalHours =  (int)startTimeSpan.TotalHours;
                TimeSpan endTimeSpan = TimeSpan.FromMilliseconds(endTime- config.Di*3);
                int endTotalHours = (int)endTimeSpan.TotalHours;
                srtWriter.WriteLine(lineNo++);
                StringBuilder sb = new ();

                sb.Append(startTotalHours >= 10 ? startTotalHours : "0" + startTotalHours);
                sb.Append(startTimeSpan.ToString(@"\:mm\:ss\.fff"));
                sb.Append(" --> ");
                sb.Append(endTotalHours >= 10 ? endTotalHours : "0" + endTotalHours);
                sb.Append(endTimeSpan.ToString(@"\:mm\:ss\.fff"));

                srtWriter.WriteLine(sb.ToString());
                srtWriter.WriteLine(ch);
                srtWriter.WriteLine();
                startTime = endTime- config.Di * 3;
                // 单词间隔补足到7T
                writer.Write(bytes, 0, bytes.Length);
                writer.Write(bytes, 0, bytes.Length);
                writer.Write(bytes, 0, bytes.Length);
                writer.Write(bytes, 0, bytes.Length);
                endTime += config.Di * 4;
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }




        /// 生成的字幕文件是一行一行的显示
        /// </summary>
        /// <param name="content"></param>
        /// <param name="keys"></param>
        /// <param name="config"></param>
        /// <param name="outPath"></param>
        /// <param name="dit_buff"></param>
        /// <param name="dah_buff"></param>
        /// <param name="useHeader"> 使用报头</param>
        /// <param name="useEnd">使用报尾</param>
        public static void ToMp3ByLine(String content,  Dictionary<char, string> keys, MorseConfig config, string outPath, short[] dit_buff, short[] dah_buff,bool useHeader = false, bool useEnd = false)
        {
            if (useEnd) { 
            
            }


            // 创建LameMP3FileWriter，设置比特率（如128kbps）
            using var writer = new LameMP3FileWriter(outPath, new WaveFormat(44100, 1), LAMEPreset.VBR_90);

            byte[] bytes = new byte[dit_buff.Length * sizeof(short)];
            byte[] di = new byte[dit_buff.Length * sizeof(short)];
            Buffer.BlockCopy(dit_buff, 0, di, 0, di.Length);
            byte[] da = new byte[dah_buff.Length * sizeof(short)];
            Buffer.BlockCopy(dah_buff, 0, da, 0, da.Length);
            if (useHeader && keys['头']!="") {
                content = "头\r\n" + content;
            }
            if (useEnd && keys['尾'] != "") {
                content =  content+ "\r\n尾";
            }
            
            //分割成每一组
            string[] lines = content.Replace("\r\n", "\n").Split('\n');

            //时间码
            const int sampleRate = 44100;
            long samplesWritten = 0;
            long subtitleStartSample = 0;
            long lineNo = 1;
            using var srtWriter = new StreamWriter(outPath.Replace(".mp3", ".srt"));
            foreach (string line in lines)
            {
                long lineAudioEndSample = subtitleStartSample;
                string[] chars = line.ToUpper().Split(' ');
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
                        foreach (char m in keys[c])
                        {
                            switch (m)
                            {
                                case '.':
                                    writer.Write(di, 0, di.Length);
                                    samplesWritten += dit_buff.Length;
                                    lineAudioEndSample = samplesWritten;
                                    break;
                                case '-':
                                    writer.Write(da, 0, da.Length);
                                    samplesWritten += dah_buff.Length;
                                    lineAudioEndSample = samplesWritten;
                                    break;
                                case ' ':
                                    writer.Write(bytes, 0, bytes.Length);
                                    writer.Write(bytes, 0, bytes.Length);
                                    samplesWritten += dit_buff.Length * 2;
                                    break;
                            }
                            // 符号间隔1T
                            writer.Write(bytes, 0, bytes.Length);
                            samplesWritten += dit_buff.Length;
                        }
                        // 字符间隔3T
                        writer.Write(bytes, 0, bytes.Length);
                        writer.Write(bytes, 0, bytes.Length);
                        writer.Write(bytes, 0, bytes.Length);
                        samplesWritten += dit_buff.Length * 3;
                    }
               
                    // 单词间隔补足到7T
                    writer.Write(bytes, 0, bytes.Length);
                    writer.Write(bytes, 0, bytes.Length);
                    writer.Write(bytes, 0, bytes.Length);
                    writer.Write(bytes, 0, bytes.Length);
                    samplesWritten += dit_buff.Length * 4;
                }
                if (lineAudioEndSample > subtitleStartSample)
                {
                    srtWriter.WriteLine(lineNo++);
                    srtWriter.WriteLine($"{ToSrtTime(subtitleStartSample, sampleRate)} --> {ToSrtTime(lineAudioEndSample, sampleRate)}");
                    srtWriter.WriteLine(line);
                    srtWriter.WriteLine();

                    // 下一组字幕在上一组有声内容结束时立即显示，覆盖行间静音等待时间。
                    subtitleStartSample = lineAudioEndSample;
                }
                writer.Write(bytes, 0, bytes.Length);
                writer.Write(bytes, 0, bytes.Length);
                writer.Write(bytes, 0, bytes.Length);
                writer.Write(bytes, 0, bytes.Length);
                samplesWritten += dit_buff.Length * 4;
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private static string ToSrtTime(long samples, int sampleRate)
        {
            TimeSpan time = TimeSpan.FromSeconds(samples / (double)sampleRate);
            return $"{(int)time.TotalHours:00}:{time.Minutes:00}:{time.Seconds:00},{time.Milliseconds:000}";
        }

        /// <summary>
        /// 
        /// 通过时长序列生成音频文件
        /// </summary>
        /// <param name="durationSequence"> 时长序列，奇数位为发声，偶数位为不发声,单位ms</param>
        /// <param name="outPath">输出文件路径</param>
        /// <param name="waveFormat">音频信息</param>
        /// <param name="frequency">频率</param>
        public static void ToMp3(List<double> durationSequence, string outPath, WaveFormat waveFormat, int frequency)
        {
            using var writer = new LameMP3FileWriter(outPath, waveFormat, LAMEPreset.VBR_90);
            for (int i = 0; i < durationSequence.Count; i++)
            {
                //采样率*持续时间=总样本数
                var dotDuration = ((int)Math.Round(durationSequence[i] * waveFormat.SampleRate)) / 1000;  // 样本数
                                                                                                          //10%的时间用来淡入
                var riseTime = dotDuration / 10;
                //10%的时间用来淡出
                var fallTime = dotDuration / 10;
                if (i % 2 == 0)
                {
                    //生成正弦波
                    for (int j = 0; j < dotDuration; j++)
                    {
                        double phase = 2 * Math.PI * frequency * j / waveFormat.SampleRate;
                        double sample = Math.Sin(phase);

                        // 淡入处理
                        if (j < riseTime)
                        {
                            double t = j / (double)riseTime;
                            sample *= Math.Pow(Math.Sin(t * Math.PI / 2), 2);
                        }

                        // 淡出处理
                        if (i >= dotDuration - fallTime)
                        {
                            int fallIndex = j - (dotDuration - fallTime);
                            double t = fallIndex / (double)(fallTime - 1);
                            sample *= Math.Pow(Math.Cos(t * Math.PI / 2), 2);
                        }

                        var buff = BitConverter.GetBytes((short)(sample * short.MaxValue));
                        writer.Write(buff, 0, buff.Length);
                    }
                }
                else
                {
                    //填充静音
                    writer.Write(new byte[dotDuration * sizeof(short)], 0, dotDuration * sizeof(short));

                }

            }
        }


        public static void ToAAC(MorsePlayer player, string outPath)
        {

            MediaFoundationEncoder.EncodeToAac(player, outPath);


        }
    }

}

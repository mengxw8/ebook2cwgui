using Microsoft.VisualBasic;
using NAudio.Lame;
using NAudio.MediaFoundation;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CW.morse
{
    internal class MorseToMp3
    {
    /// <summary>
    ///  生成mp3文件
    /// </summary>
    /// <param name="content">要生成的文本内容</param>
    /// <param name="keys">文本和嘀嗒对应关系</param>
    /// <param name="config">速度</param>
    /// <param name="outPath">输出文件路径</param>
    /// <param name="waveFormat">音频信息</param>
    /// <param name="dit_buff">嘀的波形数据</param>
    /// <param name="dah_buff">嗒的波形数据</param>
        public static void toMp3(String content, Dictionary<char, string> keys, MorseConfig config, string outPath, WaveFormat waveFormat, short[] dit_buff, short[] dah_buff)
        {

            int wpm = Math.Max(1, config.Speed); // 确保 WPM 不为 0

            var dotDuration = (50 * waveFormat.SampleRate) / (60 * wpm); // 转换为样本数

            // 创建LameMP3FileWriter，设置比特率（如128kbps）
            using (var writer = new LameMP3FileWriter(outPath, waveFormat, LAMEPreset.VBR_90))
            {


                int offset = 0;

                byte[] bytes = new byte[dotDuration * sizeof(short)];
                byte[] di = new byte[dit_buff.Length * sizeof(short)];
                Buffer.BlockCopy(dit_buff, 0, di, 0, di.Length);
                byte[] da = new byte[dah_buff.Length * sizeof(short)];
                Buffer.BlockCopy( dah_buff, 0, da, 0, da.Length);
                //分割成每一组
                string[] chars = content.Split(' ');
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
                        var code = keys[c];
                        foreach (char m in code)
                        {
                            switch (m)
                            {
                                case '.': writer.Write(di,0, di.Length); break;
                                case '-': writer.Write(da,0, da.Length); break;
                            }
                            // 符号间隔1T
                            writer.Write(bytes, 0, bytes.Length);
                        }
                        // 字符间隔3T
                        writer.Write(bytes, 0, bytes.Length);
                        writer.Write(bytes, 0, bytes.Length);
                        writer.Write(bytes, 0, bytes.Length);
                    }
                    // 单词间隔补足到7T
                    writer.Write(bytes, 0, bytes.Length);
                    writer.Write(bytes, 0, bytes.Length);
                    writer.Write(bytes, 0, bytes.Length);
                    writer.Write(bytes, 0, bytes.Length);
                }



            }

        }


    }
}

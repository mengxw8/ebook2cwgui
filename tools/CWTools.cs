using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW
{
    public class CWTools
    {

        /// <summary>
        /// 生成音频文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="param"> 调用参数</param>
        /// <returns></returns>

        public static string GenerateAudio(string fileName, string param)
        {

            ProcessStartInfo startInfo = new()
            {
                FileName = "ebook2cw.exe",
                Arguments = param,
                UseShellExecute = false,//是否使用操作系统的shell启动
                RedirectStandardOutput = true,//由调用程序获取输出信息
                CreateNoWindow = true//不显示调用程序的窗口 
            };

            try
            {
                //调用EXE
                using var process = Process.Start(startInfo);
                string result = "";
                if (process is not null)
                {
                    using var reader = process.StandardOutput;
                    // 获取exe的输出结果
                    result = reader.ReadToEnd();
                }



                if (result != "")
                {
                    string[] lines = result.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    if (lines.Length >= 2)
                    {
                        int startIndex = lines.Length - 3;
                        result = string.Join(Environment.NewLine, lines.Skip(startIndex));
                    }

                    if (result.Contains("Error:"))
                    {
                        MessageBox.Show("配置错误，转换失败，请检查！");
                    }
                    else
                    {  
                        return fileName;
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show("程序错误，请重新下载！");
            }

            return "";

        }

    }
}

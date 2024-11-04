using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CW
{
    internal class Musicplay
    {

        public static uint SND_ASYNC = 0x0001;
        public static uint SND_FILENAME = 0x00020000;
        [DllImport("winmm.dll")]
        public static extern uint mciSendString(string lpstrCommand, StringBuilder lpstrReturnString, int uReturnLength, uint hWndCallback);

        public static void PlayNmusinc(string path)
        {
            mciSendString(@"close temp_music", null, 0, 0);
            mciSendString(@"open """ + path + @""" alias temp_music", null, 0, 0);
            mciSendString("play temp_music repeat", null, 0, 0);
        }

        /// <summary>
        /// 播放音乐文件(重复)
        /// </summary>
        /// <param name="p_FileName">音乐文件名称</param>
        public static void PlayMusic_Repeat(string p_FileName)
        {
            try
            {
                mciSendString(@"close temp_music", new StringBuilder(), 0, 0);
                mciSendString(@"open " + p_FileName + " alias temp_music", new StringBuilder(), 0, 0);
                mciSendString(@"play temp_music repeat", new StringBuilder(), 0, 0);
            }
            catch
            { }
        }

        /// <summary>
        /// 播放音乐文件
        /// </summary>
        /// <param name="p_FileName">音乐文件名称</param>
        public static void PlayMusic(string p_FileName)
        {
            try
            {
                mciSendString(@"close temp_music", new StringBuilder(), 0, 0);
                //mciSendString(@"open " + p_FileName + " alias temp_music", " ", 0, 0);
                mciSendString(@"open """ + p_FileName + @""" alias temp_music", null, 0, 0);
                mciSendString(@"play temp_music", new StringBuilder(), 0, 0);
            }
            catch
            { }
        }

        /// <summary>
        /// 停止当前音乐播放
        /// </summary>
        /// <param name="p_FileName">音乐文件名称</param>
        public static void CloseMusic(string p_FileName)
        {
            try
            {
                mciSendString(@"close " + p_FileName, new StringBuilder(), 0, 0);
            }
            catch { }
        }
        public static StringBuilder Status(string p_FileName)
        {
            StringBuilder status = new StringBuilder(255);
            try
            {
           
                mciSendString(@"status movie mode" + p_FileName, status, status.Capacity, 0);
                
            }
            catch { }
            return status;
        }
        public static void PauseMusic(string p_FileName)
        {
            try
            {
                mciSendString(@"pause " + p_FileName, new StringBuilder(), 0, 0);
            }
            catch { }
        }

        public static void StopMusic()
        {
            try
            {
                mciSendString(@"stop all" , null, 0, 0);
            }
            catch { }
        }
    }
}

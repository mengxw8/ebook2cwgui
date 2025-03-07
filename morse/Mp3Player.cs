using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW
{
    internal class Mp3Player
    {
        private static WaveOutEvent? waveOut;
        private static Mp3FileReader? mp3;
        public static void Play(string path ) {
            //文件不存在，不播放
            if (!File.Exists(path))
            {
                return;
            }
            Stop();
            mp3 = new Mp3FileReader(path);
            waveOut = new ();

            Debug.WriteLine(waveOut.GetHashCode());
            waveOut.Init(mp3);
            waveOut.Play();
        }
        public static Mp3FileReader CreateMp3FileReader(string path)
        {
            //文件不存在，不播放
            if (!File.Exists(path))
            {
                return null;
            }
            Stop();
            mp3 = new Mp3FileReader(path); 
           return mp3;
        }

        public static void Pause()
        {
            if (waveOut != null && waveOut.PlaybackState == PlaybackState.Playing)
            {
                waveOut.Pause();
            }
        }
        public static void ContinuePlay()
        {
            if (waveOut != null && waveOut.PlaybackState == PlaybackState.Paused)
            {
                waveOut.Play();
            }
        }

        public static void Stop()
        {
            if (waveOut != null && waveOut.PlaybackState == PlaybackState.Playing)
            {
                waveOut.Stop();

            }
            waveOut?.Dispose();
            mp3?.Dispose();
            mp3?.Close();

        }
        public static void RePlay()
        {
            waveOut?.Dispose();
            waveOut = new WaveOutEvent();
            waveOut.Init(mp3);
            waveOut.Play();


        }


        public static void setVolume(float volume) {
            if (waveOut != null)
            {
                waveOut.Volume = volume;
            }         
        }


        public static PlaybackState Status()
        {
            if (waveOut != null)
            {
                return waveOut.PlaybackState;
            }

            return PlaybackState.Stopped;
        }
    }
}

using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW
{
    internal class Mp3Player
    {
        private static WaveOut waveOut = null;
        private static Mp3FileReader mp3 = null;
        public static void Play(string path) {
           mp3=new Mp3FileReader(path);
            waveOut = new WaveOut();
            waveOut.Init(mp3);
            waveOut.Play();
            
        }
        public static void Pause() {
            if (waveOut != null&&waveOut.PlaybackState== PlaybackState.Playing) {
                waveOut.Pause();
            } 
        }

        public static void Stop()
        {
            if (waveOut != null && waveOut.PlaybackState == PlaybackState.Playing)
            {
                waveOut.Stop();
                mp3.Close();


            }
            else if (mp3 != null) {
                mp3.Close();
            }
        }

        public static PlaybackState Status()
        {            
            if (waveOut != null  )
            {
                return waveOut.PlaybackState;
            }

            return PlaybackState.Stopped;
        }
    }
}

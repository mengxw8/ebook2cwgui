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
        private static WaveOut? waveOut ;
        private static Mp3FileReader? mp3 ;
        public static void Play(string path) {
            //文件不存在，不播放
            if (!File.Exists(path)) { 
            return;
            }
            Stop();
           mp3 =new Mp3FileReader(path);
            waveOut = new WaveOut();
            waveOut.Init(mp3);
            waveOut.Play();
            
        }
        public static void Pause() {
            if (waveOut != null&&waveOut.PlaybackState== PlaybackState.Playing) {
                waveOut.Pause();
            } 
        }        public static void ContinuePlay() {
            if (waveOut != null&&waveOut.PlaybackState== PlaybackState.Paused) {
                waveOut.Play();
            } 
        }

        public static void Stop()
        {
            if (waveOut != null && waveOut.PlaybackState == PlaybackState.Playing)
            {
                waveOut.Stop();
                waveOut.Dispose();
            }
            mp3?.Close();

        }
        public static void RePlay()
        {
            waveOut?.Dispose();
            waveOut = new WaveOut();
            waveOut.Init(mp3);
            waveOut.Play();


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

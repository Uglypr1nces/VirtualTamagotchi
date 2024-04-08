using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using NAudio.Wave;

namespace invisable_character
{
    internal class Musicplayer
    {
        private WaveOutEvent outputDevice;
        public bool isplaying;
        public Musicplayer()
        {
            outputDevice = new WaveOutEvent();
        }

        public void Playmusic(string path)
        {
            try
            {
                using (var audioFile = new AudioFileReader(path))
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();
                    isplaying = true;
                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(100);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing sound: " + ex.Message);
            }
        }
        public void Stopmusic()
        {
            if (outputDevice != null)
            {
                outputDevice.Stop();
                outputDevice.Dispose();
                outputDevice = new WaveOutEvent(); // Reinitialize for future use
                isplaying = false;
            }
        }
    }
}

using System;
using System.Threading;
using System.Windows.Forms;
using NAudio.Wave;

namespace invisable_character
{
    internal class Musicplayer
    {
        private WaveOutEvent _outputDevice;
        private AudioFileReader _audioFileReader;

        public bool IsPlaying { get; private set; }

        public Musicplayer()
        {
            IsPlaying = false;
        }

        public void Playmusic(string path)
        {
            try
            {
                _audioFileReader = new AudioFileReader(path);
                _outputDevice = new WaveOutEvent();

                _outputDevice.Init(_audioFileReader);
                _outputDevice.Play();

                IsPlaying = true;

                while (_outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing sound: " + ex.Message);
            }
        }

        public void Pause()
        {
            if (_outputDevice != null && IsPlaying)
            {
                _outputDevice.Pause();
                IsPlaying = false;
            }
        }

        public void Resume()
        {
            if (_outputDevice != null && !IsPlaying)
            {
                _outputDevice.Play();
                IsPlaying = true;
            }
        }

        public void Stop()
        {
            if (_outputDevice != null)
            {
                _outputDevice.Stop();
                IsPlaying = false;
            }
        }
    }
}

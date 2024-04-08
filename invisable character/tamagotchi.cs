using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
namespace invisable_character
{
    public class Tamagotchi
    {
        private PictureBox pictureBox;
        private Label label;
        private Panel panel1;
        private Panel panel2;
        private Timer timer;
        private String[] Playlist = new String[0];

        private Musicplayer musicplayer = new Musicplayer();
        public string TamagotchiName { get; set; }
        public int HungerLevel { get; set; }
        public int SleepLevel { get; set; }
       
        public string[] TamagotchiImages { get; set; }

        private int _currentIndex = -1;
        public Tamagotchi(string[] images,string name, int hunger, int sleep, PictureBox pictureBox, Label label, Panel panel1, Panel panel2, Timer timer)
        {
            TamagotchiName = name;
            HungerLevel = hunger;
            SleepLevel = sleep;
            TamagotchiImages = images;
            this.pictureBox = pictureBox;
            this.label = label;
            this.panel1 = panel1;
            this.panel2 = panel2;
            this.timer = timer;
            InitializeForm();
        }

        public void InitializeForm()
        {
            panel1.Visible = false;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            label.Text = $"Hello My Name is {TamagotchiName}";

            timer.Interval = 1000;
            timer.Start();
        }
        public List<T> MixList<T>(List<T> inputList)
        {
            Random rng = new Random();
            List<T> resultList = new List<T>(inputList);
            int n = resultList.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = resultList[k];
                resultList[k] = resultList[n];
                resultList[n] = value;
            }
            return resultList;
        }

        public async Task CheckHunger()
        {
            if (HungerLevel < 800000)
            {
                panel1.Visible = false;
                label.Text = "yummy food :)";
                pictureBox.Load(TamagotchiImages[3]);
                panel2.Visible = true;
                timer.Enabled = false;
                await Task.Delay(10000);
                timer.Enabled = true;
                HungerLevel += 200000;
                pictureBox.Load(TamagotchiImages[0]);
            }
            else
            {
                pictureBox.Load(TamagotchiImages[2]);
                label.Text = "im not hungry -_-";
                panel2.Visible = true;
                timer.Enabled = false;
                await Task.Delay(5000);
                timer.Enabled = true;
                pictureBox.Load(TamagotchiImages[0]);
            }
        }

        public async Task CheckSleep()
        {
            int amount_of_sleep = 0;
            panel1.Visible = false;
            label.Text = "night night";
            pictureBox.Load(TamagotchiImages[6]);
            panel2.Visible = true;
            timer.Enabled = false;

            await Task.Delay(1000);
            label.Text = "ZzzZZZ";

            while (timer.Enabled == false)
            {
                amount_of_sleep++;
                if (SleepLevel + amount_of_sleep > 100000)
                {
                    timer.Enabled = true;
                    SleepLevel = 100000;
                    amount_of_sleep = 0;
                }
            }

            await Task.Delay(100000);

            timer.Enabled = true;
            SleepLevel += amount_of_sleep;
            pictureBox.Load(TamagotchiImages[0]);
        }

        public async Task PetTamagotchi()
        {
            panel1.Visible = false;
            label.Text = "awww thanks ^^";
            pictureBox.Load(TamagotchiImages[5]);
            panel2.Visible = true;
            timer.Enabled = false;
            await Task.Delay(3000);
            timer.Enabled = true;
            pictureBox.Load(TamagotchiImages[1]);
        }
        public void StartMusic(string path)
        {
            musicplayer.Playmusic(path);
        }
        public void StopMusic()
        {
            musicplayer.Stopmusic();
        }
        public bool isplaying()
        {
            if(musicplayer.isplaying)
            {
                return true;
            }
            return false;
        }

        public async void OpenOfflineGame(string game)
        {
            if (game == "ping pong")
            {
                try
                {
                    pingpong pingpong = Application.OpenForms["pingpong"] as pingpong;
                    if (pingpong == null) { pingpong = new pingpong(); }

                    try
                    {
                        pingpong.Show();
                    }
                    catch (SocketException ex)
                    {
                        MessageBox.Show($"error opening: {ex.Message}");
                    }
                }
                catch
                {
                    MessageBox.Show("something went wrong with opening the form");
                }
            }
        }
        public async void OpenOnlineGame(string game)
        {

        }
        private void openform(string name, string pet)
        {
            
        }
    }
}

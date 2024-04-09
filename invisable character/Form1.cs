using invisable_character;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection.Emit;

namespace InvisibleCharacter
{
    public partial class Form1 : Form
    {
        // Tamagotchi
        private readonly string[] _tamagotchiPaths =
        {
            Path.Combine(Application.StartupPath, "resources/character/1.png"),
            Path.Combine(Application.StartupPath, "resources/character/2.png"),
            Path.Combine(Application.StartupPath, "resources/character/3.png"),
            Path.Combine(Application.StartupPath, "resources/character/4.png"),
            Path.Combine(Application.StartupPath, "resources/character/5.png"),
            Path.Combine(Application.StartupPath, "resources/character/6.png"),
            Path.Combine(Application.StartupPath, "resources/character/7.png")
        };

        private readonly string[] _offlineGames = { "RPS", "Guessthenumber", "ping pong" };
        private readonly string[] _onlineGames = { "RPS", "Chatify", "TicTacToe" };
        private List<string> _musicFilePaths = new List<string>();

        public string _userName { get; set; }
        public string _petname { get; set; }

        private string _typeOfGame = "";
        private string _musicFolderPath = "";
        private string _selectedBackground = Path.Combine(Application.StartupPath, "resources/background/6.png");
        private int _clickCount = 0;
        private int _tickCount = 0;
        private int _musicIndex = 0;
        private int _openOrCloseMusic = 0;

        private Tamagotchi _tamagotchi;

        public Form1(string userName, string petName)
        {
            InitializeComponent();
            _userName = userName;
            _petname = petName;
            _tamagotchi = new Tamagotchi(_tamagotchiPaths, _petname, 100000, 100000, pictureBox1, label1, panel1, panel2, timer1);
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Forms
            panel1.Visible = false;
            panel3.Visible = false;
            pictureBox1.Load(_tamagotchiPaths[0]);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            label1.Text = $"Hello {_userName}, my name is {_tamagotchi.TamagotchiName}";
            label2.Text = _petname;

            // Timer
            timer1.Interval = 1000;
            timer1.Start();

            // Progress Bar
            progressBar1.Maximum = 100000;
            progressBar2.Maximum = 100000;

            //background
            /*
            Image backgroundImage = Image.FromFile(_selectedBackground);
            panel2.BackgroundImage = backgroundImage;
            label2.Image = backgroundImage;

            foreach (Control control in Controls) //change the background of button 1-11,k
            {
                if (control is Button button)
                {
                    int buttonNumber;
                    if (int.TryParse(button.Name.Substring(6), out buttonNumber)) 
                    {
                        if (buttonNumber >= 1 && buttonNumber <= 11) 
                        {
                            button.BackgroundImage = backgroundImage;
                        }
                    }
                }
            }
            */
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            _clickCount++;
            _tamagotchi.CheckHunger();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            _clickCount++;
            _tamagotchi.CheckSleep();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            _clickCount++;
            _tamagotchi.PetTamagotchi();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _clickCount++;
            _typeOfGame = "offline";
            SetGameButtons(_offlineGames);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _clickCount++;
            _typeOfGame = "online";
            SetGameButtons(_onlineGames);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _clickCount++;
        }
        private async void button7_Click(object sender, EventArgs e)
        {
            label1.Text = "Bye byee :(";
            pictureBox1.Load(_tamagotchiPaths[1]);
            panel2.Visible = true;
            timer1.Enabled = false;
            await Task.Delay(10000);
            this.Close();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (_openOrCloseMusic == 0)
            {
                OpenMusic();
            }
            else
            {
                CloseMusic();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (_typeOfGame == "offline")
            {
                _tamagotchi.OpenOfflineGame(_offlineGames[0]);
                panel3.Visible = false;
            }
            else if (_typeOfGame == "online")
            {
                _tamagotchi.OpenOnlineGame(_onlineGames[0]);
                panel3.Visible = false;
            }
            else
            {
                if (_musicIndex <= 0) { _musicIndex = _musicFilePaths.Count - 1; }
                else { _musicIndex--; }
                ManageMusicPlayback();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (_typeOfGame == "offline")
            {
                _tamagotchi.OpenOfflineGame(_offlineGames[1]);
                panel3.Visible = false;
            }
            else if (_typeOfGame == "online")
            {
                _tamagotchi.OpenOnlineGame(_onlineGames[1]);
                panel3.Visible = false;
            }
            else
            {
                ToggleMusicPlayback();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (_typeOfGame == "offline")
            {
                _tamagotchi.OpenOfflineGame(_offlineGames[2]);
                panel3.Visible = false;
            }
            else if (_typeOfGame == "online")
            {
                _tamagotchi.OpenOnlineGame(_onlineGames[2]);
                panel3.Visible = false;
            }
            else
            {
                if (_musicIndex >= _musicFilePaths.Count - 1)
                {
                    _musicIndex = 0;
                }
                else
                {
                    _musicIndex++;
                }
                ManageMusicPlayback();
            }
        }

        private void SetGameButtons(string[] games)
        {
            button9.Text = games[0];
            button10.Text = games[1];
            button11.Text = games[2];
            panel3.Visible = true;
        }

        private void OpenMusic()
        {
            button8.Text = "Stop music";
            _openOrCloseMusic = 1;
            _typeOfGame = "music";
            button9.Text = "<";
            button10.Text = "=";
            button11.Text = ">";
            panel3.Visible = true;

            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select a folder containing music files";
                folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    _musicFolderPath = folderBrowserDialog.SelectedPath;
                }
                else
                {
                    MessageBox.Show("error using this folder");
                }
            }

            if (_musicFolderPath != null)
            {
                try
                {
                    _musicFilePaths.Clear();
                    foreach (string path in Directory.GetFiles(_musicFolderPath))
                    {
                        string extension = Path.GetExtension(path);
                        if (extension.Equals(".mp3", StringComparison.OrdinalIgnoreCase) ||
                            extension.Equals(".wav", StringComparison.OrdinalIgnoreCase))
                        {
                            _musicFilePaths.Add(path);
                        }
                    }
                    _musicFilePaths = _tamagotchi.MixList(_musicFilePaths);
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show("Unauthorized access to the folder.");
                }
                catch (DirectoryNotFoundException ex)
                {
                    MessageBox.Show("Folder not found.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }

        private void CloseMusic()
        {
            button8.Text = "Start music";
            panel3.Visible = false;
            _openOrCloseMusic = 0;
        }

        private void ManageMusicPlayback()
        {
            try
            {
                _tamagotchi.StopMusic();
                Task.Run(() => _tamagotchi.StartMusic(_musicFilePaths[_musicIndex]));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"index = {_musicIndex}");
            }
        }

        private void ToggleMusicPlayback()
        {
            if (_tamagotchi.isplaying())
            {
                _tamagotchi.StopMusic();
            }
            else
            {
                try
                {
                    Task.Run(() => _tamagotchi.StartMusic(_musicFilePaths[_musicIndex]));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error starting music: {ex.Message}");
                }
            }
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            _clickCount++;
            panel1.Visible = _clickCount % 2 != 0;
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            _tickCount++;
            _tamagotchi.HungerLevel -= 100;
            _tamagotchi.SleepLevel -= 100;

            progressBar1.Value = _tamagotchi.HungerLevel;
            progressBar2.Value = _tamagotchi.SleepLevel;

            if (_tickCount > 10)
            {
                label1.Text = "";
                panel2.Visible = false;
            }

            if (_tamagotchi.HungerLevel < 50000)
            {
                pictureBox1.Load(_tamagotchiPaths[1]);
                label1.Text = "I'm kinda hungry...";
                panel2.Visible = true;
                timer1.Enabled = false;
                await Task.Delay(5000);
                timer1.Enabled = true;
            }

            if (_tamagotchi.SleepLevel < 20000)
            {
                pictureBox1.Load(_tamagotchiPaths[1]);
                label1.Text = "I'm kinda sleepy...";
                panel2.Visible = true;
                timer1.Enabled = false;
                await Task.Delay(5000);
                timer1.Enabled = true;
            }
        }

    }
}

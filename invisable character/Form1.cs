using invisable_character;
using System;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

        private readonly string[] _musicFilePaths = { };
        private readonly string[] _offlineGames = { "RPS", "Guessthenumber", "ping pong" };
        private readonly string[] _onlineGames = { "RPS", "Chatify", "TicTacToe" };

        private string _musicFolderPath = "";
        private string _userName = "shpat";
        private int _clickCount = 0;
        private int _tickCount = 0;
        private Tamagotchi _tamagotchi;
        private string _typeOfGame = "";

        public Form1()
        {
            InitializeComponent();
            _tamagotchi = new Tamagotchi(_tamagotchiPaths, "BOB", 100000, 100000, pictureBox1, label1, panel1, panel2, timer1);
            InitializeForm();
        }

        public void InitializeForm()
        {
            // Forms
            panel1.Visible = false;
            panel3.Visible = false;
            pictureBox1.Load(_tamagotchiPaths[0]);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            label1.Text = $"Hello {_userName} My Name is {_tamagotchi.TamagotchiName}";

            // Timer
            timer1.Interval = 1000;
            timer1.Start();

            // Progress Bar
            progressBar1.Maximum = 100000;
            progressBar2.Maximum = 100000;
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
            button9.Text = "RPS";
            button10.Text = "Guessthenumber";
            button11.Text = "ping pong";
            panel3.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _clickCount++;

            _typeOfGame = "online";
            button9.Text = "RPS";
            button10.Text = "Chatify";
            button11.Text = "TicTacToe";
            panel3.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _clickCount++;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            _clickCount++;
            timer1.Stop();
            panel1.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            _typeOfGame = "music";
            button9.Text = "<";
            button10.Text = "=";
            button11.Text = ">";
            panel3.Visible = true;

            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select a folder containing music files";
                folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK) { _musicFolderPath = folderBrowserDialog.SelectedPath; }
            }
            foreach(string path in Directory.GetFiles(_musicFolderPath))
            {
                if(path.Contains(".mp3") || path.Contains(".wav")){
                    _musicFilePaths.Append(path);
                }
            }
            _tamagotchi.MusicPlayer(_musicFilePaths);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (_typeOfGame == "offline")
            {
                _tamagotchi.OpenGame(_offlineGames[0]);
                panel3.Visible = false;
            }
            else if (_typeOfGame == "online")
            {
                _tamagotchi.OpenGame(_onlineGames[0]);
                panel3.Visible = false;
            }
            else
            {

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (_typeOfGame == "offline")
            {
                _tamagotchi.OpenGame(_offlineGames[1]);
                panel3.Visible = false;
            }
            else if (_typeOfGame == "online")
            {
                _tamagotchi.OpenGame(_onlineGames[1]);
                panel3.Visible = false;
            }
            else
            {

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (_typeOfGame == "offline")
            {
                _tamagotchi.OpenGame(_offlineGames[2]);
                panel3.Visible = false;
            }
            else if (_typeOfGame == "online")
            {
                _tamagotchi.OpenGame(_onlineGames[2]);
                panel3.Visible = false;
            }
            else
            {

            }
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            _clickCount++;
            if (_clickCount % 2 != 0) { panel1.Visible = true; }
            else { panel1.Visible = false; }
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
        }
    }
}

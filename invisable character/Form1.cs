using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace invisable_character
{
    public partial class Form1 : Form
    {
        //tamagotchi
        public string[] tamagotchiPaths = new string[]
        {
            Path.Combine(Application.StartupPath, "resources/character/1.png"),
            Path.Combine(Application.StartupPath, "resources/character/2.png"),
            Path.Combine(Application.StartupPath, "resources/character/3.png"),
            Path.Combine(Application.StartupPath, "resources/character/4.png"),
            Path.Combine(Application.StartupPath, "resources/character/5.png"),
            Path.Combine(Application.StartupPath, "resources/character/6.png"),
            Path.Combine(Application.StartupPath, "resources/character/7.png")
        };


        string user_name = "nigga";
        int click_count = 0; 
        int tick_count = 0; 
        private Tamagotchi tamagotchi;
        public Form1()
        {
            InitializeComponent();
            tamagotchi = new Tamagotchi(tamagotchiPaths,"BOB", 0, 0, pictureBox1, label1, panel1, panel2, timer1);
            InitializeFrom();
        }
        public void InitializeFrom()
        {
            //forms
            panel1.Visible = false;
            pictureBox1.Load(tamagotchiPaths[0]);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            label1.Text = $"Hello {user_name} My Name is {tamagotchi.TamagotchiName}";

            //timer
            timer1.Interval = 1000;
            timer1.Start();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            click_count++;
            tamagotchi.CheckHunger();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            click_count++;
            tamagotchi.CheckSleep();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            click_count++;
            tamagotchi.PetTamagotchi();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            click_count++;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            click_count++;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            click_count++;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            click_count++;
            timer1.Stop();
            panel1.Visible = false;
            this.Close();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            click_count++;
            if (click_count % 2 != 0) { panel1.Visible = true; }
            else { panel1.Visible = false; }
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            tick_count++;
            tamagotchi.HungerLevel++;
            tamagotchi.SleepLevel++;

            if (tick_count > 10)
            {
                label1.Text = "";
                panel2.Visible = false;
            }
            if (tamagotchi.HungerLevel > 50000)
            {
                pictureBox1.Load(tamagotchiPaths[1]);
                label1.Text = "im kinda hungry...";
                panel2.Visible = true;
                timer1.Enabled = false;
                await Task.Delay(5000);
                timer1.Enabled = true;
            }
        }
    }
}
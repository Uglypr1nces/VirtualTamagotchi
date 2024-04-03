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
        public string tamagotchi_normal = Path.Combine(Application.StartupPath, "resources/character/1.png");
        public string tamagotchi_surprised = Path.Combine(Application.StartupPath, "resources/character/2.png");
        public string tamagotchi_angry = Path.Combine(Application.StartupPath, "resources/character/3.png");
        public string tamagotchi_happy = Path.Combine(Application.StartupPath, "resources/character/4.png");
        public string tamagotchi_cool = Path.Combine(Application.StartupPath, "resources/character/5.png");
        public string tamagotchi_cute = Path.Combine(Application.StartupPath, "resources/character/6.png");
        public string tamagotchi_sleeping = Path.Combine(Application.StartupPath, "resources/character/7.png");

        public string tamagotchi_name = "sam";
        public string user_name = "shpat";

        //tamagotchi attributes
        public int tickCount = 0;
        public int click_count = 0;
        public int sleep_level = 0;
        public int hunger = 0;
        public Form1()
        {
            InitializeComponent();

            //forms
            panel1.Visible = false;
            pictureBox1.Load(tamagotchi_normal);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            label1.Text = $"Hello {user_name} My Name is {tamagotchi_name}";

            //timer
            timer1.Interval = 1000;
            timer1.Start();
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            click_count++;
            if (hunger > 100000)
            {
                panel1.Visible = false;
                hunger = 0;
                label1.Text = "yummy food :)";
                pictureBox1.Load(tamagotchi_happy);
                panel2.Visible = true;
                timer1.Enabled = false;
                await Task.Delay(10000);
                timer1.Enabled = true;
                pictureBox1.Load(tamagotchi_normal);
            }
            else
            {
                pictureBox1.Load(tamagotchi_angry);
                label1.Text = "im not hungry -_-";
                panel2.Visible = true;
                timer1.Enabled = false;
                await Task.Delay(5000);
                timer1.Enabled = true;
                pictureBox1.Load(tamagotchi_normal);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            click_count++;
            panel1.Visible = false;
            label1.Text = "night night";
            pictureBox1.Load(tamagotchi_sleeping);
            panel2.Visible = true;
            timer1.Enabled = false;
            await Task.Delay(1000);
            label1.Text = "ZzzZZZ";
            await Task.Delay(100000);
            timer1.Enabled = true;
            pictureBox1.Load(tamagotchi_normal);
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            click_count++;
            panel1.Visible = false;
            label1.Text = "awww thanks ^^";
            pictureBox1.Load(tamagotchi_cute);
            panel2.Visible = true;
            timer1.Enabled = false;
            await Task.Delay(3000);
            timer1.Enabled = true;
            pictureBox1.Load(tamagotchi_normal);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            click_count++;
            panel1.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            click_count++;
            panel1.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            click_count++;
            panel1.Visible = false;
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
            tickCount++;
            hunger++;
            sleep_level++;

            if (tickCount > 10)
            {
                label1.Text = "";
                panel2.Visible = false;
            }
            if (hunger > 50000)
            {
                pictureBox1.Load(tamagotchi_surprised);
                label1.Text = "im kinda hungry...";
                panel2.Visible = true;
                timer1.Enabled = false;
                await Task.Delay(5000);
                timer1.Enabled = true;
            }
        }
    }
}
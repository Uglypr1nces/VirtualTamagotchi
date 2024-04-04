using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace invisable_character
{
    public class Tamagotchi
    {
        private PictureBox pictureBox;
        private Label label;
        private Panel panel1;
        private Panel panel2;
        private Timer timer;

        public string TamagotchiName { get; set; }
        public int HungerLevel { get; set; }
        public int SleepLevel { get; set; }
        public string[] TamagotchiImages { get; set; }

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

        public async Task CheckHunger()
        {
            if (HungerLevel > 100000)
            {
                panel1.Visible = false;
                HungerLevel = 0;
                label.Text = "yummy food :)";
                pictureBox.Load(TamagotchiImages[3]);
                panel2.Visible = true;
                timer.Enabled = false;
                await Task.Delay(10000);
                timer.Enabled = true;
                pictureBox.Load("resources/character/normal.png");
            }
            else
            {
                pictureBox.Load(TamagotchiImages[2]);
                label.Text = "im not hungry -_-";
                panel2.Visible = true;
                timer.Enabled = false;
                await Task.Delay(5000);
                timer.Enabled = true;
                pictureBox.Load("resources/character/normal.png");
            }
        }

        public async Task CheckSleep()
        {
            panel1.Visible = false;
            label.Text = "night night";
            pictureBox.Load(TamagotchiImages[6]);
            panel2.Visible = true;
            timer.Enabled = false;
            await Task.Delay(1000);

            label.Text = "ZzzZZZ";
            await Task.Delay(100000);
            timer.Enabled = true;
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
    }
}

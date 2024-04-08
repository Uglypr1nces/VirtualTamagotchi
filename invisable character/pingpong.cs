using System;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace invisable_character
{
    public partial class pingpong : Form
    {

        public int speedplayer = 25;
        public int speedballtop = 3;
        public int speedballleft = 3;
        public int score = 0;

        public pingpong()
        {
            InitializeComponent();
            KeyDown += new KeyEventHandler(Form1_KeyDown); // Attach KeyDown event handler
        }

        public void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && pictureBox1.Location.X >= 0)
            {
                pictureBox1.Left -= speedplayer;
            }
            else if (e.KeyCode == Keys.Right && pictureBox1.Location.X <= 839)
            {
                pictureBox1.Left += speedplayer;
            }
            if (e.KeyCode == Keys.A && pictureBox1.Location.X >= 0)
            {
                pictureBox1.Left -= speedplayer;
            }
            else if (e.KeyCode == Keys.D && pictureBox1.Location.X <= 839)
            {
                pictureBox1.Left += speedplayer;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random randomtop = new Random();
            int randomballtop = randomtop.Next(1, 2);

            Random randomleft = new Random();
            int randomballleft = randomleft.Next(1, 2);

            pictureBox2.Top += speedballtop;
            pictureBox2.Left += speedballleft;
            label2.Text = Convert.ToString(score);

            if (pictureBox2.Top < 0)
            {
                speedballtop = -speedballtop;
            }

            if (pictureBox2.Left < 0)
            {
                speedballleft = -speedballleft;
            }

            if (pictureBox2.Right > 1125)
            {
                speedballleft = -speedballleft;
            }

            if (pictureBox2.Bottom >= pictureBox1.Top && pictureBox2.Bottom <= pictureBox1.Bottom && pictureBox2.Left >= pictureBox1.Left && pictureBox2.Right <= pictureBox1.Right)
            {
                speedballtop += randomballtop;
                speedballleft += randomballleft;
                speedballtop = -speedballtop;
                score++;
            }

            if (pictureBox2.Bottom > pictureBox1.Bottom + 100)
            {
                timer1.Enabled = false;
                label3.Visible = true;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            label3.Visible = false;
            pictureBox2.Top = 300;
            pictureBox2.Left = 300;
            speedballleft = 3;
            speedballtop = 3;
            score = 0;
        }
    }
}

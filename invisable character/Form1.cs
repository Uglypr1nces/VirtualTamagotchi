using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace invisable_character
{
    public partial class Form1 : Form
    {
        public int count = 0;
        public Form1()
        {
            InitializeComponent();
            panel1.Visible = false;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            count ++;
            if (count % 2 != 0){panel1.Visible = true;}
            else{ panel1.Visible = false; }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            count++;
            panel1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            count ++;
            panel1.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            count++;
            panel1.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            count++;
            panel1.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            count++;
            panel1.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            count++;
            panel1.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            count++;
            panel1.Visible = false;
            this.Close();
        }
    }
}

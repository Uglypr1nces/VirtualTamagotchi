using InvisibleCharacter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace invisable_character
{
    public partial class OptionsForm : Form
    {
        public int currentstep = 0;
        public string username = "";
        public string petname = "";
        public OptionsForm()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && textBox1.Text == " ")
            {
                MessageBox.Show("please fill the textbox");
            }
            else
            {
                if (currentstep == 0)
                {
                    username = textBox1.Text;
                    label1.Text = "Name for your pet";
                    currentstep++;
                }
                else
                {
                    petname = textBox1.Text;
                    currentstep++;

                    MessageBox.Show($"username = {username}, petname = {petname}");
                    openform(username,petname);
                }
            }
        
            textBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "Whats your name";
            if (currentstep > 0)
            {
                currentstep--;
            }
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("https://github.com/Uglypr1nces");
            Process.Start(sInfo);
        }

        private void openform(string name, string pet)
        {
            try
            {
                Form1 form1 = Application.OpenForms["Form1"] as Form1;
                if (form1 == null) { form1 = new Form1(username,petname); }
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(petname))
                {
                    try
                    {
                        form1._userName = name;
                        form1.petname = pet;
                        form1.Show();
                        this.Hide();
                    }
                    catch (SocketException ex)
                    {
                        MessageBox.Show($"error opening: {ex.Message}");
                    }
                }
            }
            catch
            {
                MessageBox.Show("something went wrong with opening the form");
            }
        }
    }
}
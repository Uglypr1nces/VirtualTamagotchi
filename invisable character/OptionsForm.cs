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

        private database mydatabase = new database(Path.Combine(Application.StartupPath, "resources/database/user.csv"));
        public OptionsForm()
        {
            InitializeComponent();
            if (mydatabase.UserExists())
            {
                openform(mydatabase.recieveUsername(), mydatabase.recievePetname());   
            }
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            this.Dispose();
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
                    mydatabase.adduser(username, petname);
                    currentstep++;
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
                Form1 form1 = Application.OpenForms.OfType<Form1>().FirstOrDefault();
                if (form1 == null)
                {
                    form1 = new Form1(name, pet);
                }
                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(pet))
                {
                    try
                    {
                        if (!form1.Visible)
                        {
                            form1._userName = name;
                            form1._petname = pet;
                            form1.Show();
                        }
                        else
                        {
                            form1.Show();
                        }

                        this.Hide();
                    }
                    catch (SocketException ex)
                    {
                        MessageBox.Show($"Error opening: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex}");
            }
        }

    }
}
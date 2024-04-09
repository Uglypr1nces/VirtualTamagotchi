using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace invisable_character
{
    internal class database
    {
        public string csv_file_path;
        public database(string path) 
        {
            csv_file_path = path;
        }
        public bool UserExists()
        {
            try
            {
                var lines = File.ReadLines(csv_file_path);

                foreach (var line in lines.Skip(1))
                {
                    var values = line.Split(',');

                    string existingUsername = values[0].Trim();

                    if (existingUsername != "")
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while reading the CSV file: {csv_file_path}");
                return false;
            }
        }
        public bool adduser(string username, string petname)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(csv_file_path))
                {
                    sw.WriteLine($"{username},{petname}");
                }

                MessageBox.Show("User added successfully.");

                return true; // Return true upon successful addition
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the user: {ex.Message}");
                return false;
            }
        }

        public string recieveUsername()
        {
            try
            {
                var lines = File.ReadLines(csv_file_path);

                foreach (var line in lines.Skip(1))
                {
                    var values = line.Split(',');

                    string existingUsername = values[0].Trim();

                    if (existingUsername != "")
                    {
                        return existingUsername;
                    }
                }

                return "notfound";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while reading the CSV file: {ex.Message}");
                return "error";
            }
        }
        public string recievePetname()
        {
            try
            {
                var lines = File.ReadLines(csv_file_path);

                foreach (var line in lines.Skip(1))
                {
                    var values = line.Split(',');

                    string existingPetname = values[1].Trim();

                    if (existingPetname != "")
                    {
                        return existingPetname;
                    }
                }

                return "notfound";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while reading the CSV file: {ex.Message}");
                return "error";
            }
        }
    }
}

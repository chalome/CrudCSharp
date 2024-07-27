using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using rappel.Controleur;
using rappel.Modele;
namespace rappel
{
    public partial class Form1 : Form
    {
        private RegistrationController controller;
        private bool connected;

        public Form1()
        {
            InitializeComponent();
            controller = new RegistrationController();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "se connecter")
            {
                try
                {
                    controller.Connect();
                    button1.Text = "se deconnecter";
                    connected = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                controller.Disconnect();
                button1.Text = "se connecter";
                connected = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (connected)
            {
                // Validate input fields
                if (string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) ||
                    string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox6.Text) ||
                    comboBox1.SelectedItem == null || string.IsNullOrEmpty(textBox5.Text))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                // Validate date format
                DateTime date;
                if (!DateTime.TryParse(textBox4.Text, out date))
                {
                    MessageBox.Show("Please enter a valid date.");
                    return;
                }

                var registration = new Registration
                {
                    Nom = textBox2.Text,
                    Prenom = textBox3.Text,
                    Date = date,
                    Adresse = textBox6.Text,
                    Gender = comboBox1.SelectedItem.ToString(),
                    Country = textBox5.Text
                };
                controller.AddRegistration(registration);
                MessageBox.Show("Saved successfully!");
            }
            else
            {
                MessageBox.Show("Please connect to the database first.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (connected)
            {
                listView1.Items.Clear();
                var registrations = controller.GetRegistrations();
                foreach (var reg in registrations)
                {
                    listView1.Items.Add(new ListViewItem(new[]
                    {
                        reg.Id.ToString(), reg.Nom, reg.Prenom, reg.Date.ToString("yyyy-MM-dd"),
                        reg.Adresse, reg.Gender, reg.Country
                    }));
                }
            }
            else
            {
                MessageBox.Show("Please connect to the database first.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (connected)
            {
                // Validate ID field
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Please enter an ID.");
                    return;
                }

                // Declare the variable separately
                int id;
                if (!int.TryParse(textBox1.Text, out id))
                {
                    MessageBox.Show("Please enter a valid ID.");
                    return;
                }


                // Validate other fields
                if (string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) ||
                    string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox6.Text) ||
                    comboBox1.SelectedItem == null || string.IsNullOrEmpty(textBox5.Text))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                // Validate date format
                 DateTime date;
        if (!DateTime.TryParse(textBox4.Text, out date))
        {
            MessageBox.Show("Please enter a valid date.");
            return;
        }

                var registration = new Registration
                {
                    Id = id,
                    Nom = textBox2.Text,
                    Prenom = textBox3.Text,
                    Date = date,
                    Adresse = textBox6.Text,
                    Gender = comboBox1.SelectedItem.ToString(),
                    Country = textBox5.Text
                };
                controller.UpdateRegistration(registration);
                MessageBox.Show("Updated successfully!");
            }
            else
            {
                MessageBox.Show("Please connect to the database first.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (connected)
            {
                // Validate ID field
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Please enter an ID.");
                    return;
                }

                // Declare the variable separately
                int id;
                if (!int.TryParse(textBox1.Text, out id))
                {
                    MessageBox.Show("Please enter a valid ID.");
                    return;
                }


                controller.DeleteRegistration(id);
                MessageBox.Show("Deleted successfully!");
            }
            else
            {
                MessageBox.Show("Please connect to the database first.");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.SelectedItem = null;
            textBox6.Text = "";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }
    }
    
}

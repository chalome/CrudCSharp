using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace rappel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        MySqlConnection con = new MySqlConnection("server=localhost;port=3306;database=mycsharp;uid=root;pwd=");
        bool connecte;

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "se connecter")
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                        button1.Text = "se deconnecter";
                        connecte = true;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
            else
            {
                con.Close();
                button1.Text = "se connecter";
                connecte = false;

            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            if (connecte)
            {
                MySqlCommand save = new MySqlCommand("insert into registration(nom,prenom,date,adresse,gender,country) values(@nom,@prenom,@date,@adresse,@gender,@country)", con);
                //save.Parameters.AddWithValue("@id", textBox1.Text);
                save.Parameters.AddWithValue("@nom", textBox2.Text);
                save.Parameters.AddWithValue("@prenom", textBox3.Text);
                save.Parameters.AddWithValue("@date", textBox4.Text);
                save.Parameters.AddWithValue("@adresse", textBox6.Text);
                save.Parameters.AddWithValue("@gender", comboBox1.SelectedItem.ToString());
                save.Parameters.AddWithValue("@country", textBox5.Text);
                save.ExecuteNonQuery();
                save.Parameters.Clear();
                MessageBox.Show("saved!!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (connecte)
            {
                listView1.Items.Clear();
                MySqlCommand show = new MySqlCommand("select * from registration ", con);
                using (MySqlDataReader reader = show.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string ID = reader["id"].ToString();
                        string NOM = reader["nom"].ToString();
                        string PRENOM = reader["prenom"].ToString();
                        string DATE = reader["date"].ToString();
                        string ADRESSE = reader["adresse"].ToString();
                        string GENDER = reader["gender"].ToString();
                        string COUNTRY = reader["country"].ToString();
                        listView1.Items.Add(new ListViewItem(new[] { ID, NOM, PRENOM, DATE,ADRESSE, GENDER, COUNTRY }));
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (connecte)
            {
                MySqlCommand update = new MySqlCommand("update  registration set nom='" + textBox2.Text + "',prenom='" + textBox3.Text + "',date='" + textBox4.Text + "',adresse='" + textBox6.Text + "',gender='" + comboBox1.SelectedItem.ToString() + "',country='" + textBox5.Text + "'where id=" + textBox1.Text + "", con);

                update.Parameters.AddWithValue("@id", textBox1.Text);
                update.Parameters.AddWithValue("@nom", textBox2.Text);
                update.Parameters.AddWithValue("@prenom", textBox3.Text);
                update.Parameters.AddWithValue("@date", textBox4.Text);
                update.Parameters.AddWithValue("@adresse", textBox6.Text);
                update.Parameters.AddWithValue("@gender", comboBox1.SelectedItem.ToString());
                update.Parameters.AddWithValue("@country", textBox5.Text);
                update.ExecuteNonQuery();
                update.Parameters.Clear(); 
                MessageBox.Show("well done");
            }
            else
            {
                MessageBox.Show("noooo");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (connecte)
            {
                MySqlCommand delete = new MySqlCommand("delete  from registration where id="+textBox1.Text+"",con);
                delete.ExecuteNonQuery();
                MessageBox.Show("deleted");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.SelectedItem = "";
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

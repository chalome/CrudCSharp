using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using rappel.Modele;


namespace rappel.Controleur
{
    class RegistrationController
    {
        private MySqlConnection con;

        public RegistrationController()
        {
            con = new MySqlConnection("server=localhost;port=3306;database=mycsharp;uid=root;pwd=");
        }

        public void Connect()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
        }

        public void Disconnect()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }

        public void AddRegistration(Registration registration)
        {
            string query = "INSERT INTO registration(nom, prenom, date, adresse, gender, country) VALUES(@nom, @prenom, @date, @adresse, @gender, @country)";
            using (MySqlCommand cmd = new MySqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@nom", registration.Nom);
                cmd.Parameters.AddWithValue("@prenom", registration.Prenom);
                cmd.Parameters.AddWithValue("@date", registration.Date);
                cmd.Parameters.AddWithValue("@adresse", registration.Adresse);
                cmd.Parameters.AddWithValue("@gender", registration.Gender);
                cmd.Parameters.AddWithValue("@country", registration.Country);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Registration> GetRegistrations()
        {
            List<Registration> registrations = new List<Registration>();
            string query = "SELECT * FROM registration";
            using (MySqlCommand cmd = new MySqlCommand(query, con))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        registrations.Add(new Registration
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Nom = reader["nom"].ToString(),
                            Prenom = reader["prenom"].ToString(),
                            Date = Convert.ToDateTime(reader["date"]),
                            Adresse = reader["adresse"].ToString(),
                            Gender = reader["gender"].ToString(),
                            Country = reader["country"].ToString()
                        });
                    }
                }
            }
            return registrations;
        }

        public void UpdateRegistration(Registration registration)
        {
            string query = "UPDATE registration SET nom=@nom, prenom=@prenom, date=@date, adresse=@adresse, gender=@gender, country=@country WHERE id=@id";
            using (MySqlCommand cmd = new MySqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@id", registration.Id);
                cmd.Parameters.AddWithValue("@nom", registration.Nom);
                cmd.Parameters.AddWithValue("@prenom", registration.Prenom);
                cmd.Parameters.AddWithValue("@date", registration.Date);
                cmd.Parameters.AddWithValue("@adresse", registration.Adresse);
                cmd.Parameters.AddWithValue("@gender", registration.Gender);
                cmd.Parameters.AddWithValue("@country", registration.Country);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteRegistration(int id)
        {
            string query = "DELETE FROM registration WHERE id=@id";
            using (MySqlCommand cmd = new MySqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}

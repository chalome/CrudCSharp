using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rappel.Modele
{
    class Registration
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime Date { get; set; }
        public string Adresse { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
    }
}

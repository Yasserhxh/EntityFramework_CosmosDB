using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class FilterModel
    {
        public string date { get; set; }
        public string statut { get; set; }
        public string priorite { get; set; }
        public string validateur { get; set; }
        public string equipe { get; set; }
        public string resultat { get; set; }
        public string declarationID { get; set; }
    }
}

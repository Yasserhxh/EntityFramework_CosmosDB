using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class DeclarationModel
    {
        public string Dclaration_ID { get; set; }
        public DateTime? Declaration_Date { get; set; }
        public string Declaration_Adresse { get; set; }
        public string Declaration_Ville { get; set; }
        public string Declaration_Localisation { get; set; }
        public string Declaration_LongLat { get; set; }
        public string Declaration_Photo { get; set; }
        public string Declaration_Statut { get; set; }
        public string Declaration_Validateur { get; set; }
        public DateTime? Declaration_DateValidation { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
    public class InterventionModel
    {
        public string Intervention_ID { get; set; }
        public string Intervention_DeclarationID { get; set; }
        public DateTime? Intervention_Date { get; set; }
        public string Intervention_Equipe { get; set; }
        public string Intervention_Resultat { get; set; }
        public string Intervention_Commentaire { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class DeclarationModel
    {
        [JsonPropertyName("declaration_id")]
        [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Dclaration_ID { get; set; }
        public DateTime? Declaration_Date { get; set; }
        public string Declaration_Adresse { get; set; }
        public string Declaration_Ville { get; set; }
        public string Declaration_Latitude { get; set; }
        public string Declaration_Longitude { get; set; }
        public string Declaration_Photo { get; set; }
        public string Declaration_Statut { get; set; }
        public string Declaration_Validateur { get; set; }
        public DateTime? Declaration_DateValidation { get; set; }
        // public IList<Intervention> Interventions { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
   
}

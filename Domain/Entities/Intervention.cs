using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Intervention
    {
        [JsonPropertyName("Intervention_ID")]
        [Key]
        //  [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Intervention_ID { get; set; }
        public string Intervention_DeclarationID { get; set; }
        public DateTime? Intervention_Date { get; set; }
        public string Intervention_Equipe { get; set; }
        public string Intervention_Resultat { get; set; }
        public string Intervention_Priorite { get; set; }
        public string Intervention_Commentaire { get; set; }
        //public Declaration Declaration { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }


    }
}

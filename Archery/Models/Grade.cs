using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Archery.Models
{
    [Serializable]
    public partial class Grade
    {
        public Grade()
        {
            Progression = new HashSet<Progression>();
            Question = new HashSet<Question>();
            Resultat = new HashSet<Resultat>();
        }

        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [JsonIgnore]
        public virtual ICollection<Progression> Progression { get; set; }
        [JsonIgnore]
        public virtual ICollection<Question> Question { get; set; }
        [JsonIgnore]
        public virtual ICollection<Resultat> Resultat { get; set; }
    }
}

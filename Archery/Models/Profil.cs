using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Archery.Models
{
    [Serializable]
    public partial class Profil
    {
        
        public Profil()
        {
            Progression = new HashSet<Progression>();
            Resultat = new HashSet<Resultat>();
        }

        public int Id { get; set; }
        [Range(1, 4)]
        public int? Genre { get; set; }
        [Required]
        public string Nom { get; set; }
        public string Couleur { get; set; }
        public bool? EstDroitier { get; set; }
        [JsonIgnore]
        public virtual ICollection<Progression> Progression { get; set; }
        [JsonIgnore]
        public virtual ICollection<Resultat> Resultat { get; set; }
    }
}

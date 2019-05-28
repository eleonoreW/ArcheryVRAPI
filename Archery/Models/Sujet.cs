using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Archery.Models
{
    public partial class Sujet
    {
        public Sujet()
        {
            Question = new HashSet<Question>();
        }

        public int Id { get; set; }
        public string Nom { get; set; }
        [JsonIgnore]
        public virtual ICollection<Question> Question { get; set; }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace Archery.Models
{
    public partial class Question
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "scholarLevel")]
        [Required]
        public int GradeId { get; set; }
        [JsonProperty(PropertyName = "subject")]
        [Required]
        public int SujetId { get; set; }
        [JsonProperty(PropertyName = "question")]
        [Required]
        public string QuestionText { get; set; }
        [JsonProperty(PropertyName = "difficulty")]
        [Required]
        [Range(1, 4)]
        public int Difficult { get; set; }
        [JsonProperty(PropertyName = "Explanation")]
        [Required]
        public string Explanation { get; set; }
        [JsonProperty(PropertyName = "listAnswer")]
        [Required]
        public string GoodAnswers { get; set; }
        [JsonProperty(PropertyName = "listBadAnswer")]
        [Required]
        public string BadAnswers { get; set; }
        [JsonIgnore]
        public virtual Grade Grade { get; set; }
        [JsonIgnore]
        public virtual Sujet Sujet { get; set; }
    }
}

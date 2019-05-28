using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Archery.Models
{
    [Serializable]
    public partial class Progression
    {
        public int Id { get; set; }
        public int GradeId { get; set; }
        public int ProfilId { get; set; }
        public int? DifficulteMaths { get; set; }
        public int? Xpmaths { get; set; }
        public int? DifficulteFrancais { get; set; }
        public int? Xpfrancais { get; set; }
        public int? DifficulteAnglais { get; set; }
        public int? Xpanglais { get; set; }
        [JsonIgnore]
        public virtual Grade Grade { get; set; }
        [JsonIgnore]
        public virtual Profil Profil { get; set; }

        /*[{"id":1,"gradeId":3,"profilId":0,"difficulteMaths":4,"xpmaths":5,"difficulteFrancais":4,"xpfrancais":5,"difficulteAnglais":4,"xpanglais":5,"grade":null,"profil":null}]*/
    }
}

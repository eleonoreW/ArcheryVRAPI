using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Archery.Models
{
    [Serializable]
    public partial class Resultat
    {
        public int Id { get; set; }
        [Required]
        public int ProfilId { get; set; }
        [Required]
        public int GradeId { get; set; }
        [Required]
        public DateTime DateResultat { get; set; }
        public int? DifficulteMaths { get; set; }
        public double? ResMaths { get; set; }
        public int? DifficulteFrancais { get; set; }
        public double? ResFrancais { get; set; }
        public int? DifficulteAnglais { get; set; }
        public double? ResAnglais { get; set; }

        public virtual Grade Grade { get; set; }
        public virtual Profil Profil { get; set; }

        /*[{"id":1,"profilId":0,"gradeId":3,"dateResultat":"2019-05-09T00:00:00","difficulteMaths":2,"resMaths":20.0,"difficulteFrancais":20,"resFrancais":45.0,"difficulteAnglais":45,"resAnglais":45.0,"grade":null,"profil":null}]*/
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Archery.Models
{
    public class QuestionWithList : Question
    {
        [JsonProperty(PropertyName = "listAnswer")]
        public string[] ListGood { get; set; }

        [JsonProperty(PropertyName = "listBadAnswer")]
        public string[] ListBad { get; set; }

        public QuestionWithList(int gradeId, int sujetId, string questionText, int difficult, string explanation, string listGood, string listBad)
        {
            GradeId = gradeId;
            SujetId = sujetId;
            QuestionText = questionText;
            Difficult = difficult;
            Explanation = explanation;
            ListGood = listGood.Split(",");
            ListBad = listBad.Split(",");
        }
    }
}




            /*
[Serializable]
    public class Quizz
    {
        public ScholarLevel scolarLevel;
        [Range(1, 3)]
        public int difficulty = 1;
        public Subject subject;
        public string question;
        public string Explanation;
        public List<string> listAnswer;
        public List<string> listBadAnswer;

        // TODO
        public Quizz(string question, string listAnswer, string listBadAnswer, ScholarLevel scolarLevel, int difficulty, Subject subject)
        {


        }

        public Quizz(string question, List<string> listAnswer, List<string> listBadAnswer, ScholarLevel scolarLevel, int difficulty, Subject subject)
        {
            this.question = question;
            this.listAnswer = listAnswer;
            this.listBadAnswer = listBadAnswer;
            this.scolarLevel = scolarLevel;
            this.difficulty = difficulty;
            this.subject = subject;
        }
    }

    public enum ScholarLevel
    {
        CM1,
        CM2,
        SIXIEME,
        CINQUIEME,
    }

    public enum Subject
    {
        FRANCAIS,
        MATHS,
        ANGLAIS,
    }
}*/
   
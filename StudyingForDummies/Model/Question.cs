using GalaSoft.MvvmLight;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace StudyingForDummies.Model
{
    class Question
    {
        public Question()
        {
            Answers = new List<Answer>();
        }
        public int Id { get; set; }
        public string Text { get; set; }
        public List<Answer> Answers { get; set; }
    }
}

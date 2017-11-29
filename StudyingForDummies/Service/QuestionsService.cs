using Newtonsoft.Json.Linq;
using StudyingForDummies.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StudyingForDummies.Service
{
    class QuestionsService
    {
        private IEnumerable<Question> _allQuestions;
        private IEnumerable<Answer> _allAnswers;

        public QuestionsService()
        {
            _allQuestions = GetAllQuestions();
            _allAnswers = GetAllAnswers();
        }
        public string CheckResult(IEnumerable<Question> questions)
        {
            int countSuccess = 0;
            int countFail = 0;

            foreach (var question in questions)
            {
                var answer = question.Answers.FirstOrDefault(x => x.IsSelected);
                var rightAnswer = _allAnswers.FirstOrDefault(x => x.Id == question.Id);
                if (answer == null)
                {
                    countFail++;
                    continue;
                }
                else if (answer.Id == rightAnswer.RightAnswer)
                    countSuccess++;
                else
                    countFail++;
            }
            return $"Right answers: {countSuccess}; Wrong answers: {countFail}";
        }
        public IEnumerable<Question> GetQuestions(int count)
        {
            var random = new Random();
            var questionsCount = _allQuestions.Count();
            var questions = new List<Question>();
            int counter = 0;
            while (counter < count)
            {
                var randomNumber = random.Next(0, questionsCount);
                var randomQuestion = _allQuestions.ElementAt(randomNumber);
                if (!questions.Contains(randomQuestion))
                {
                    questions.Add(randomQuestion);
                    counter++;
                }
            }
            return questions;
        }
        public IEnumerable<Answer> GetAllAnswers()
        {
            var json = File.ReadAllText(@".\Keys.json");
            var answers = new List<Answer>();
            var resultObjects = AllChildren(JObject.Parse(json))
            .First(c => c.Type == JTokenType.Array && c.Path.Contains("keys"))
            .Children<JObject>();
            foreach (JObject result in resultObjects)
            {
                var answer = new Answer();
                foreach (JProperty property in result.Properties())
                {
                    if (property.Name == "number")
                        answer.Id = (int)property.Value;
                    if (property.Name == "key")
                        answer.RightAnswer = (int)property.Value;
                }
                answers.Add(answer);
            }
            return answers;
        }
        public IEnumerable<Question> GetAllQuestions()
        {
            var json = File.ReadAllText(@".\Questions.json");
            var questions = new List<Question>();
            var resultObjects = AllChildren(JObject.Parse(json))
            .First(c => c.Type == JTokenType.Array && c.Path.Contains("questions"))
            .Children<JObject>();

            foreach (JObject result in resultObjects)
            {
                var question = new Question();
                foreach (JProperty property in result.Properties())
                {
                    if (property.Name == "number")
                        question.Id = (int)property.Value;
                    if (property.Name == "alternatives")
                    {
                        var alternatives = property.Value.ToObject<List<string>>();
                        int i = 0;
                        foreach (var alternative in alternatives)
                            question.Answers.Add(new Answer(i++, alternative));
                    }

                    if (property.Name == "text")
                        question.Text = (string)property.Value;
                }
                questions.Add(question);
            }
            return questions;
        }
        private static IEnumerable<JToken> AllChildren(JToken json)
        {
            foreach (var c in json.Children())
            {
                yield return c;
                foreach (var cc in AllChildren(c))
                {
                    yield return cc;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Quiz7Mojos.Server.Services
{
    public class QuestionService : BaseService, IQuestionService
    {
        public ICollection<Question> GetQuestionsByCategory(int count, string category, int difficulty)
        {
            var questions = GetData().Select(x => x)
                .Where(x => x.Category.Equals(category) && x.Rank == difficulty)
                .Take(count)
                .ToList();

            return questions;
        }

        public ICollection<Question> GetQuestionsNormalMode(List<string> categories, int difficulty)
        {
            var questions = GetData().Select(x => x)
                .Where(x => x.Rank == difficulty && categories.Contains(x.Category))
                .OrderBy(x => Guid.NewGuid())
                .Take(10)
                .ToList();

            return questions;
        }

        public ICollection<Question> GetQuestionsSurvivalMode(List<string> categories)
        {
            var questionPerCategory = GetData().Count / categories.Count;
            var questions = new List<Question>();
            while (categories.Count != 0)
            {
                var currentCategory = categories.First();

                var easyQuestions = GetData().Select(x => x)
                .Where(x => x.Rank == 0 && x.Category.Equals(currentCategory))
                .Take(questionPerCategory)
                .OrderBy(x => Guid.NewGuid())
                .ToList();

                var mediumQuestions = GetData().Select(x => x)
                .Where(x => x.Rank == 1 && x.Category.Equals(currentCategory))
                .Take(2)
                .OrderBy(x => Guid.NewGuid())
                .ToList();

                var hardQuestions = GetData().Select(x => x)
                .Where(x => x.Rank == 2 && x.Category.Equals(currentCategory))
                .Take(2)
                .OrderBy(x => Guid.NewGuid())
                .ToList();

                questions.AddRange(easyQuestions);
                questions.AddRange(mediumQuestions);
                questions.AddRange(hardQuestions);

                if (categories.Count != 0)
                {
                    categories.Remove(currentCategory);
                }
            }

            return questions;
        }

        public string GetCorrectAnswerFromQuestion(int id)
        {
            string correctAnswer = null;
            try
            {
                var question = GetData().First(x => x.Id == id);
                if (question != null)
                {
                    var correctAnswerId = question.CorrectAnswer;
                    correctAnswer = question.PossibleAnswers[correctAnswerId];
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return correctAnswer;
        }
    }
}

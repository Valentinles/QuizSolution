using Quiz7Mojos.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace Quiz7Mojos.Server.Services
{
    public class ScoreService : BaseService, IScoreService
    {
        public QuizResult CalculateNormalModeScore(List<QuestionAnswer> questionAnswers)
        {
            var result = new QuizResult();
            if (questionAnswers.Count != 0)
            {
                foreach (var questionAnswer in questionAnswers)
                {
                    var question = GetData().FirstOrDefault(x => x.Id == questionAnswer.QuestionId);
                    var correctAnswer = question.PossibleAnswers[question.CorrectAnswer];
                    if (correctAnswer.Equals(questionAnswer.UserAnswer))
                    {
                       result.Score++;
                    }
                    else
                    {
                        result.WrongAnswers++;
                    }
                }
            }
            return result;
        }

        public QuizResult CalculateSurvivalModeScore(List<QuestionAnswer> questionAnswers)
        {
            var result = new QuizResult();
            if (questionAnswers.Count != 0)
            {
                foreach (var questionAnswer in questionAnswers)
                {
                    var question = GetData().FirstOrDefault(x => x.Id == questionAnswer.QuestionId);
                    var correctAnswer = question.PossibleAnswers[question.CorrectAnswer];
                    if (correctAnswer.Equals(questionAnswer.UserAnswer))
                    {
                        result.Score++;
                    }
                    else
                    {
                        result.WrongAnswers++;
                        break;
                    }
                }
            }
            return result;
        }
    }
}

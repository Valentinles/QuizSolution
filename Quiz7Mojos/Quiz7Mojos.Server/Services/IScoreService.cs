using Quiz7Mojos.Common.Models;
using System.Collections.Generic;

namespace Quiz7Mojos.Server.Services
{
    public interface IScoreService
    {
        QuizResult CalculateNormalModeScore(List<QuestionAnswer> questionAnswers);
        QuizResult CalculateSurvivalModeScore(List<QuestionAnswer> questionAnswers);
    }
}

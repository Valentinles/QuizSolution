using System.Collections.Generic;

namespace Quiz7Mojos.Server.Services
{
    public interface IQuestionService
    {
        ICollection<Question> GetQuestionsByCategory(int count, string category, int difficulty);
        ICollection<Question> GetQuestionsNormalMode(List<string> categories, int difficulty);
        ICollection<Question> GetQuestionsSurvivalMode(List<string> categories);
        string GetCorrectAnswerFromQuestion(int id);
    }
}

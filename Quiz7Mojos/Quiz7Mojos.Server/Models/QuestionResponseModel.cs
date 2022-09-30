using System.Collections.Generic;

namespace Quiz7Mojos.Server.Models
{
    public class QuestionResponseModel
    {
        public int Id { get; set; }

        public string Category { get; set; }

        public int Rank { get; set; }

        public string Text { get; set; }

        public List<string> PossibleAnswers { get; set; }
    }
}

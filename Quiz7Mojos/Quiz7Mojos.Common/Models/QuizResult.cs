using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Quiz7Mojos.Common.Models
{
    public class QuizResult
    {
        [JsonPropertyName("score")]
        public int Score { get; set; }

        [JsonPropertyName("wrongAnswers")]
        public int WrongAnswers { get; set; }
    }
}

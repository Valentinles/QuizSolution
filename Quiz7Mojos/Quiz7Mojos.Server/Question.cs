using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Quiz7Mojos.Server
{
    public class Question
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("rank")]
        public int Rank { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("possibleAnswers")]
        public List<string> PossibleAnswers { get; set; }

        [JsonPropertyName("correctAnswer")]
        public int CorrectAnswer { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Quiz7Mojos.Client.Models
{
    public class GetQuestionsModel
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("difficulty")]
        public int Difficulty { get; set; }

        [JsonPropertyName("categories")]
        public string[] Categories { get; set; }
    }
}

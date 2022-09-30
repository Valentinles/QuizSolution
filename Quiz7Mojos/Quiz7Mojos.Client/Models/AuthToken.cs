using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Quiz7Mojos.Client.Models
{
    public class AuthToken
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}

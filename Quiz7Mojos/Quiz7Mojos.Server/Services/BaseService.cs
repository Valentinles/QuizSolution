using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Quiz7Mojos.Server.Services
{
    public class BaseService
    {
        protected ICollection<Question> GetData()
        {
            var result = new List<Question>();
            using (StreamReader reader = new StreamReader("questions.json"))
            {
                string json = reader.ReadToEnd();
                result = JsonConvert.DeserializeObject<List<Question>>(json);
            }
            return result;
        }
    }
}

using Newtonsoft.Json;
using Quiz7Mojos.Client.Models;
using Quiz7Mojos.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Quiz7Mojos.Client
{
    public class QuizApiService
    {
        private readonly HttpClient httpClient;
        public QuizApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            httpClient.BaseAddress = new Uri("https://localhost:44319/");
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", GetToken().Result);
        }

        public async Task<string> GetCorrectAnswer(int questionId)
        {
            string result;
            try
            {
                var response = await this.httpClient.GetAsync($"question/getcorrectanswer?questionId={questionId}");
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                result = jsonString;
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public async Task<List<CategoryViewModel>> GetCategories()
        {
            List<CategoryViewModel> categories = null;
            try
            {
                var categoriesResponse = await this.httpClient.GetAsync("category/getcategories");
                categoriesResponse.EnsureSuccessStatusCode();

                var jsonString = await categoriesResponse.Content.ReadAsStringAsync();
                categories = JsonConvert.DeserializeObject<List<CategoryViewModel>>(jsonString); ;
            }
            catch (Exception)
            {

                throw;
            }
            return categories;
        }

        public async Task<List<QuestionViewModel>> GetNormalModeQuestions(string[] categories, int difficulty)
        {
            List<QuestionViewModel> questions = null;
            try
            {
                string json = JsonConvert.SerializeObject(categories);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var questionsResponse = await this.httpClient.PostAsync($"question/getquestionsnormalmode?difficulty={difficulty}", content);
                questionsResponse.EnsureSuccessStatusCode();

                var jsonString = await questionsResponse.Content.ReadAsStringAsync();
                questions = JsonConvert.DeserializeObject<List<QuestionViewModel>>(jsonString); ;
            }
            catch (Exception)
            {

                throw;
            }
            return questions;
        }

        public async Task<QuizResult> GetNormalModeScore(List<QuestionAnswer> questionAnswers)
        {
            QuizResult result;
            try
            {
                string json = JsonConvert.SerializeObject(questionAnswers);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await this.httpClient.PostAsync($"score/calculatenormalmodescore", content);

                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<QuizResult>(jsonString);
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public async Task<List<QuestionViewModel>> GetSurvivalModeQuestions(string[] categories)
        {
            List<QuestionViewModel> questions;
            try
            {
                string json = JsonConvert.SerializeObject(categories);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var questionsResponse = await this.httpClient.PostAsync("question/getquestionssurvivalmode", content);
                questionsResponse.EnsureSuccessStatusCode();

                var jsonString = await questionsResponse.Content.ReadAsStringAsync();
                questions = JsonConvert.DeserializeObject<List<QuestionViewModel>>(jsonString); ;
            }
            catch (Exception)
            {

                throw;
            }
            return questions;
        }

        public async Task<QuizResult> GetSurvivalModeScore(List<QuestionAnswer> questionAnswers)
        {
            QuizResult result;
            try
            {
                string json = JsonConvert.SerializeObject(questionAnswers);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await this.httpClient.PostAsync($"score/calculatesurvivalmodescore", content);

                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<QuizResult>(jsonString);
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public async Task<string> GetToken()
        {
            AuthToken token;
            try
            {
                var tokenResponse = await this.httpClient.GetAsync("auth/gettoken");
                tokenResponse.EnsureSuccessStatusCode();

                var jsonString = await tokenResponse.Content.ReadAsStringAsync();
                token = JsonConvert.DeserializeObject<AuthToken>(jsonString);
            }
            catch (Exception)
            {

                throw;
            }
            return token.Token;
        }
    }
}

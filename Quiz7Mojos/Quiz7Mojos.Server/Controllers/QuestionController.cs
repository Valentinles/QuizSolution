using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quiz7Mojos.Server.Models;
using Quiz7Mojos.Server.Services;
using System.Collections.Generic;
using System.Linq;

namespace Quiz7Mojos.Server.Controllers
{
    public class QuestionController : ApiController
    {
        private readonly IQuestionService questionService;
        public QuestionController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        [HttpGet]
        [Route(nameof(GetQuestions))]
        public IActionResult GetQuestions(int count, string category, int difficulty)
        {
            var questions = this.questionService.GetQuestionsByCategory(count, category, difficulty);

            if (questions.Any())
            {
                var response = new List<QuestionResponseModel>();
                foreach (var question in questions)
                {
                    var responseModel = new QuestionResponseModel()
                    {
                        Id = question.Id,
                        Category = question.Category,
                        Rank = question.Rank,
                        Text = question.Text,
                        PossibleAnswers = question.PossibleAnswers

                    };
                    response.Add(responseModel);
                }
                return Ok(response);
            }

            return NotFound("No questions match the current criteria.");
        }

        [HttpPost]
        [Route(nameof(GetQuestionsNormalMode))]
        public IActionResult GetQuestionsNormalMode([FromBody] List<string> categories, int difficulty)
        {
            var questions = this.questionService.GetQuestionsNormalMode(categories, difficulty);

            if (questions.Any())
            {
                var response = new List<QuestionResponseModel>();
                foreach (var question in questions)
                {
                    var responseModel = new QuestionResponseModel()
                    {
                        Id = question.Id,
                        Category = question.Category,
                        Rank = question.Rank,
                        Text = question.Text,
                        PossibleAnswers = question.PossibleAnswers

                    };
                    response.Add(responseModel);
                }
                return Ok(response);
            }

            return NotFound("No questions match the current criteria.");
        }

        [HttpPost]
        [Route(nameof(GetQuestionsSurvivalMode))]
        public IActionResult GetQuestionsSurvivalMode([FromBody] List<string> categories)
        {
            var questions = this.questionService.GetQuestionsSurvivalMode(categories);

            if (questions.Any())
            {
                var response = new List<QuestionResponseModel>();
                foreach (var question in questions)
                {
                    var responseModel = new QuestionResponseModel()
                    {
                        Id = question.Id,
                        Category = question.Category,
                        Rank = question.Rank,
                        Text = question.Text,
                        PossibleAnswers = question.PossibleAnswers

                    };
                    response.Add(responseModel);
                }
                return Ok(response);
            }

            return NotFound("No questions match the current criteria.");
        }

        [HttpGet]
        [Route(nameof(GetCorrectAnswer))]
        public IActionResult GetCorrectAnswer(int questionId)
        {
            var correctAnswer = this.questionService.GetCorrectAnswerFromQuestion(questionId);
            if (correctAnswer != null)
            {
                return Ok(correctAnswer);
            }
            return NotFound("Question not found.");
        }
    }
}

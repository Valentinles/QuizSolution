using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quiz7Mojos.Common.Models;
using Quiz7Mojos.Server.Services;
using System;
using System.Collections.Generic;

namespace Quiz7Mojos.Server.Controllers
{
    public class ScoreController : ApiController
    {
        private readonly IScoreService scoreService;
        public ScoreController(IScoreService scoreService)
        {
            this.scoreService = scoreService;
        }

        [HttpPost]
        [Route(nameof(CalculateNormalModeScore))]
        public IActionResult CalculateNormalModeScore(List<QuestionAnswer> questionAnswers)
        {
            object score = null; 
            try
            {
                score = this.scoreService.CalculateNormalModeScore(questionAnswers);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            return Ok(score);
        }

        [HttpPost]
        [Route(nameof(CalculateSurvivalModeScore))]
        public IActionResult CalculateSurvivalModeScore(List<QuestionAnswer> questionAnswers)
        {
            object score = null;
            try
            {
                score = this.scoreService.CalculateSurvivalModeScore(questionAnswers);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            return Ok(score);
        }
    }
}

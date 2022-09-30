using Quiz7Mojos.Client.Models;
using Quiz7Mojos.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Quiz7Mojos.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();
            QuizApiService apiSerivce = null;

            try
            {
                apiSerivce = new QuizApiService(client);
            }
            catch (Exception ex)
            {
                Console.WriteLine("The server is offline.");
                Environment.Exit(0);
            }

            Console.WriteLine("Welcome to 7Mojos Quiz!");

            Console.WriteLine("Please select the categories you want to play: (ex. biology,world history)");
            PrintCategories(apiSerivce);
            var categoriesInput = Console.ReadLine();
            var selectedCategories = categoriesInput.Split(",");

            Console.WriteLine("Please select the mode you want to play: (Normal or Survival) ");
            var modeInput = Console.ReadLine();
            while (modeInput != "Normal" && modeInput != "Survival")
            {
                Console.WriteLine("Please type Normal or Survival");
                modeInput = Console.ReadLine();
            }

            //NORMAL MODE
            int difficulty = -1;
            if (modeInput == "Normal")
            {
                var difficulties = new Dictionary<string, int>()
                {
                    {"Easy", 0},
                    {"Medium", 1},
                    {"Hard", 2},
                };
                Console.WriteLine("Please select the difficulty of the question: (Easy/Medium/Hard)");
                var difficultyInput = Console.ReadLine();
                while (!difficulties.ContainsKey(difficultyInput))
                {
                    Console.WriteLine("Please select from the avaiable difficulties!");
                    difficultyInput = Console.ReadLine();
                }
                difficulty = difficulties.First(x => x.Key.Equals(difficultyInput)).Value;
                Console.WriteLine();
                var userAnswers = GenerateNormalUserQuiz(apiSerivce, selectedCategories, modeInput, difficulty);
                PrintResults(apiSerivce, userAnswers, modeInput);
            }

            //Survival
            if (modeInput == "Survival")
            {
                var userAnswers = GenerateSurvivalUserQuiz(apiSerivce, selectedCategories);
                PrintResults(apiSerivce, userAnswers, modeInput);
            }

        }

        private static void PrintResults(QuizApiService apiSerivce, List<QuestionAnswer> userAnswers, string mode)
        {
            if (mode == "Normal")
            {
                var score = apiSerivce.GetNormalModeScore(userAnswers).Result;
                Console.WriteLine($"Congratulations you've finished the normal quiz with {score.Score} correct and {score.WrongAnswers} wrong answers!");
                Console.WriteLine($"Your final score is {score.Score}!");
            }
            else
            {
                var score = apiSerivce.GetSurvivalModeScore(userAnswers).Result;
                Console.WriteLine("The answer is wrong!");
                Console.WriteLine($"You've finished the survival quiz with score of {score.Score}!");
            }
        }

        private static List<QuestionAnswer> GenerateNormalUserQuiz(QuizApiService apiSerivce, string[] selectedCategories, string modeInput, int difficulty)
        {
            List<QuestionViewModel> questions = apiSerivce.GetNormalModeQuestions(selectedCategories, difficulty).Result;

            var userAnswers = new List<QuestionAnswer>();
            foreach (var q in questions)
            {
                var userAnswer = new QuestionAnswer();
                Console.WriteLine(q.Text);
                foreach (var ans in q.PossibleAnswers)
                {
                    Console.WriteLine(ans);
                }
                Console.WriteLine();

                Console.WriteLine("Please type your choice: ");
                var inputAnswer = Console.ReadLine();
                while (!q.PossibleAnswers.Contains(inputAnswer))
                {
                    Console.WriteLine("Your input does not match the available answers, please type it again.");
                    inputAnswer = Console.ReadLine();
                }
                userAnswer.QuestionId = q.Id;
                userAnswer.UserAnswer = inputAnswer;
                userAnswers.Add(userAnswer);
            }

            return userAnswers;
        }

        private static List<QuestionAnswer> GenerateSurvivalUserQuiz(QuizApiService apiSerivce, string[] selectedCategories)
        {
            List<QuestionViewModel> questions = apiSerivce.GetSurvivalModeQuestions(selectedCategories).Result;

            var userAnswers = new List<QuestionAnswer>();
            foreach (var q in questions)
            {
                var userAnswer = new QuestionAnswer();
                Console.WriteLine(q.Text);
                foreach (var ans in q.PossibleAnswers)
                {
                    Console.WriteLine(ans);
                }
                Console.WriteLine();

                Console.WriteLine("Please type your choice: ");
                var inputAnswer = Console.ReadLine();
                while (!q.PossibleAnswers.Contains(inputAnswer))
                {
                    Console.WriteLine("Your input does not match the available answers, please type it again.");
                    inputAnswer = Console.ReadLine();
                }
                userAnswer.QuestionId = q.Id;
                userAnswer.UserAnswer = inputAnswer;
                userAnswers.Add(userAnswer);
                var correctAnswer = apiSerivce.GetCorrectAnswer(userAnswer.QuestionId).Result;
                if (userAnswer.UserAnswer != correctAnswer)
                {
                    break;
                }
            }

            return userAnswers;
        }

        private static void PrintCategories(QuizApiService apiSerivce)
        {
            var categories = apiSerivce.GetCategories().Result;
            foreach (var category in categories)
            {
                Console.Write(category.Category + "\t");
            }
            Console.WriteLine();
        }
    }
}

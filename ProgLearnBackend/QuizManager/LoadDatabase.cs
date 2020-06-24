using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using QuizManager.Database.Models;
using QuizManager.Database.RepositoryContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace QuizManager
{
    public class LoadDatabase : BackgroundService
    {
        private readonly IUnitOfWork _unitOfWork;
        private int _fetchCounter = 0;
        private string baseURL = "https://opentdb.com/api.php?amount=10&category=23&difficulty=easy&type=multiple";

        public LoadDatabase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            List<string> questions = await GetQuizQuestions();
        }

        private async Task<List<string>> GetQuizQuestions()
        {
            List<string> questions = new List<string>();
            

            bool success = false;
            while (!success)
            {
                try
                {
                    using HttpResponseMessage res = await new HttpClient().GetAsync(baseURL);
                    string data = await res.Content.ReadAsStringAsync();
                    questions.AddRange(JObject.Parse(data)["results"].Select(item => Convert.ToString(item["question"])).ToList());
                    success = true;
                }
                catch (HttpRequestException hre)
                {
                    Console.WriteLine($"Exception thrown getting page data: {baseURL}");
                    Console.WriteLine(hre.Message);
                }
            }
            Console.WriteLine(questions.Count());

            return questions;
        }

        private async Task Update(List<string> questions)
        {
            List<string> questionsFromDb = await _unitOfWork.QuizRepository.GetAllQuestions();

            foreach (var question in questions)
            {
                if (!questionsFromDb.Contains(question))
                {
                    try
                    {
                        bool succes = false;
                        while (!succes)
                        {
                            try
                            {
                                using HttpResponseMessage res = await new HttpClient().GetAsync(baseURL);
                                string data = await res.Content.ReadAsStringAsync();
                                JToken jsonObject = JObject.Parse(data);
                                await AddNewQuiz(jsonObject);
                                await AddNewIncorrectAnswer(jsonObject);
                                await AddNewQuizIncorrectAnswer(jsonObject);
                                succes = true;
                            }
                            catch (HttpRequestException hre)
                            {
                                Console.WriteLine($"Exception thrown getting page data: {baseURL}");
                                Console.WriteLine(hre.Message);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex is ArgumentNullException || ex is InvalidOperationException)
                        {
                            Console.WriteLine($"Exception thrown getting page data: {baseURL}");
                            Console.WriteLine(ex.Message);
                            ++_fetchCounter;
                            continue;
                        }
                    }
                    Console.WriteLine("Fetch was needed");
                }
                ++_fetchCounter;
                Console.WriteLine(_fetchCounter);
            }
        }

        private async Task AddNewQuiz(JToken jsonObject)
        {
            await _unitOfWork.QuizRepository.AddAsync(new Database.Models.Quiz()
            {
                Category = Convert.ToString(jsonObject["category"]),
                Type = Convert.ToString(jsonObject["type"]),
                Difficulty = Convert.ToString(jsonObject["difficulty"]),
                Question = Convert.ToString(jsonObject["question"]),
                CorrectAnswer = Convert.ToString(jsonObject["correct_answer"]),
            });
            await _unitOfWork.SaveAsync();
        }

        private async Task AddNewIncorrectAnswer(JToken jsonObject)
        {
            List<string> incorrectAnswersFromDb = await _unitOfWork.IncorrectAnswerRepository.GetAllOptions();

            foreach (var incorrect in jsonObject["incorrect_answers"])
            {
                string currentOption = Convert.ToString(incorrect);

                if (!incorrectAnswersFromDb.Contains(currentOption))
                {
                    await _unitOfWork.IncorrectAnswerRepository.AddAsync(new IncorrectAnswer() { Option = currentOption });
                    await _unitOfWork.SaveAsync();
                }
            }
        }

        private async Task AddNewQuizIncorrectAnswer(JToken jsonObject)
        {
            foreach (var incorrect in jsonObject["incorrect_answers"])
            {
                int currentQuizId = await _unitOfWork.QuizRepository.GetIdByQuestion(Convert.ToString(jsonObject["question"]));
                int currentOptionId = await _unitOfWork.IncorrectAnswerRepository.GetIdByOption(Convert.ToString(incorrect));

                if (!await _unitOfWork.QuizIncorrectAnswerRepository.DoesPairExist(currentQuizId, currentOptionId))
                {
                    await _unitOfWork.QuizIncorrectAnswerRepository.AddAsync(new QuizIncorrectAnswer()
                    {
                        IncorrectAnswerId = currentOptionId,
                        QuizId = currentQuizId
                    });
                    await _unitOfWork.SaveAsync();
                }

            }
        }
    }
}

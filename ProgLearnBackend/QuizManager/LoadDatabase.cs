using DataAccessLibrary.RepositoryContainer;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QuizManager
{
    public class LoadDatabase : BackgroundService
    {
        private readonly IUnitOfWork _unitOfWork;
        private int _fetchCounter = 0;

        public LoadDatabase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }

        private async Task Update(List<int> movieIds)
        {
            List<int> questionsFromDb = await _unitOfWork.QuizRepository.GetAllQuetions();

            foreach (var movieId in movieIds)
            {
                if (!movieIdsFromDb.Contains(movieId))
                {
                    string dynamicURL = $"https://api.themoviedb.org/3/movie/{movieId}?api_key=bb29364ab81ef62380611d162d85ecdb&language=en-US";
                    try
                    {
                        bool succes = false;
                        while (!succes)
                        {
                            try
                            {
                                using HttpResponseMessage res = await new HttpClient().GetAsync(dynamicURL);
                                string data = await res.Content.ReadAsStringAsync();
                                JToken jsonObject = JObject.Parse(data);
                                await AddNewMovie(jsonObject);
                                await AddNewGenres(jsonObject);
                                await AddNewMovieGenre(jsonObject);
                                await AddNewLanguages(jsonObject);
                                await AddNewMovieLanguages(jsonObject);
                                succes = true;
                            }
                            catch (HttpRequestException hre)
                            {
                                Console.WriteLine($"Exception thrown getting page data: {dynamicURL}");
                                Console.WriteLine(hre.Message);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex is ArgumentNullException || ex is InvalidOperationException)
                        {
                            Console.WriteLine($"Exception thrown getting page data: {dynamicURL}");
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
    }
}

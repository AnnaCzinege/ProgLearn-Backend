using Grpc.Net.Client;
using QuizManager;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Quiz.QuizClient(channel);

            var request = new GetQuizByCategoryAndDifficultyDTO { Category = "History", Difficulty = "medium" };
            var reply = await client.GetQuizzesByCategoryAndDifficultyAsync(request);

            //var request = new GetQuizDTO { QuizId = 130 };

            //var reply = await client.GetQuizByIdAsync(request);

            foreach (var item in reply.Quizzes)
            {
                List<string> options = new List<string>();

                foreach (var option in item.IncorrectAnswers)
                {
                    options.Add(option.Option);
                }

                Console.WriteLine($"\nCategory: {item.Category}\nDifficulty: {item.Difficulty}\n" +
                    $"Question: {item.Question}\nAnswer: {item.Answer}\nIncorrect Answers: {options[0]}, {options[1]}, {options[2]}");
            }

            

            Console.ReadLine();
        }
    }
}

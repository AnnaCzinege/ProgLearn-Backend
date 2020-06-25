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
            var request = new GetQuizDTO { QuizId = 130 };

            var reply = await client.GetQuizByIdAsync(request);

            List<string> options = new List<string>();

            foreach (var item in reply.IncorrectAnswers)
            {
                options.Add(item.Option);
            }

            Console.WriteLine($"Category: {reply.Category}\nDifficulty: {reply.Difficulty}\n" +
                $"Question: {reply.Question}\nAnswer: {reply.Answer}\nIncorrect Answers: {options[0]}, {options[1]}, {options[2]}");

            Console.ReadLine();
        }
    }
}

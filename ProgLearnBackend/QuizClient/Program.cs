using Grpc.Net.Client;
using QuizManager;
using System;
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

            Console.WriteLine($"Id: {reply.QuizId}\nCategory: {reply.Category}\nDifficulty: {reply.Difficulty}\nQuestion: {reply.Question}\nAnswer: {reply.Answer}");

            Console.ReadLine();
        }
    }
}

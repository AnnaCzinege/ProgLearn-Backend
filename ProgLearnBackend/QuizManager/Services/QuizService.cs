using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Services
{
    public class QuizService : Quiz.QuizBase
    {

        public async Task<QuizDTO> GetQuiz(int userId, int quizId)
        {
            return null;
        }
    }
}

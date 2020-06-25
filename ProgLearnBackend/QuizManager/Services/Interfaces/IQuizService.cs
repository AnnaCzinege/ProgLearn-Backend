using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Services.Interfaces
{
    public interface IQuizService
    {
        Task<QuizDTO> GetQuiz(int userId, int quizId);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuizManager.Database.Repositories.Interfaces
{
    public interface IQuizRepository : IGenericRepository<Models.Quiz>
    {
        Task<List<string>> GetAllQuestions();
        Task<int> GetIdByQuestion(string question);
        Task<Models.Quiz> GetQuizById(int quizId);
    }
}

using QuizManager.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Database.Repositories.Interfaces
{
    public interface IQuizIncorrectAnswerRepository : IGenericRepository<QuizIncorrectAnswer>
    {
        Task<bool> DoesPairExist(int quizId, int optionId);
    }
}

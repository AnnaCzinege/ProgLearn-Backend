using QuizManager.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Database.Repositories.Interfaces
{
    public interface IIncorrectAnswerRepository : IGenericRepository<IncorrectAnswer>
    {
        Task<List<string>> GetAllOptions();
        Task<int> GetIdByOption(string option);
        Task<List<string>> GetIncorrectAnswers(List<int> incorrectAnswerIds);
    }
}

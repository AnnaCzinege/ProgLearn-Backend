using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repositories
{
    public interface IQuizRepository : IGenericRepository<Quiz>
    {
        Task<List<string>> GetAllQuestions();
    }
}

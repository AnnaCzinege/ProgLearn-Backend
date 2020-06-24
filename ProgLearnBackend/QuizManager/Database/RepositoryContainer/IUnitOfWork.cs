using DataAccessLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.RepositoryContainer
{
    public interface IUnitOfWork
    {
        IQuizRepository QuizRepository { get;}
        Task SaveAsync();
    }
}

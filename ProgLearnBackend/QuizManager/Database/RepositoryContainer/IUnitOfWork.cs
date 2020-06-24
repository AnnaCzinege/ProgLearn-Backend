using QuizManager.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuizManager.Database.RepositoryContainer
{
    public interface IUnitOfWork
    {
        IQuizRepository QuizRepository { get;}
        IIncorrectAnswerRepository IncorrectAnswerRepository { get; }
        IQuizIncorrectAnswerRepository QuizIncorrectAnswerRepository { get; }
        Task SaveAsync();
    }
}

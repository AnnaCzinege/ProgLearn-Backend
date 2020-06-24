using QuizManager.Database.DataAccess;
using QuizManager.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuizManager.Database.RepositoryContainer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QuizContext _context;
        public IQuizRepository QuizRepository { get; set; }
        public IIncorrectAnswerRepository IncorrectAnswerRepository { get; set; }
        public IQuizIncorrectAnswerRepository QuizIncorrectAnswerRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public IUserQuizRepository UserQuizRepository { get; set; }

        public UnitOfWork(QuizContext context, 
            IQuizRepository quizRepository, 
            IIncorrectAnswerRepository incorrectAnswerRepository,
            IQuizIncorrectAnswerRepository quizIncorrectAnswerRepository,
            IUserQuizRepository userQuizRepository,
            IUserRepository userRepository)
        {
            _context = context;
            QuizRepository = quizRepository;
            IncorrectAnswerRepository = incorrectAnswerRepository;
            QuizIncorrectAnswerRepository = quizIncorrectAnswerRepository;
            UserRepository = userRepository;
            UserQuizRepository = userQuizRepository;
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

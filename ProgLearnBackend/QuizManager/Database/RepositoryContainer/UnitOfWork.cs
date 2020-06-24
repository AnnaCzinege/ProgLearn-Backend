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
        public IUserRepository UserRepository { get; set; }

        IQuizRepository IUnitOfWork.QuizRepository => throw new NotImplementedException();

        public UnitOfWork(QuizContext context, 
            IQuizRepository quizRepository, 
            IUserRepository userRepository)
        {
            _context = context;
            QuizRepository = quizRepository;
            UserRepository = userRepository;
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

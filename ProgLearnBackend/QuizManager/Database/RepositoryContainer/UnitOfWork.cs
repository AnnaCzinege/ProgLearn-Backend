using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.RepositoryContainer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QuizContext _context;
        public IQuizRepository QuizRepository { get; set; }
        public IUserRepository UserRepository { get; set; }

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

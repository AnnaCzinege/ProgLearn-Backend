using QuizManager.Database.Models;
using QuizManager.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Database.Repositories.SQL
{
    public class QuizIncorrectAnswerRepository : GenericRepository<QuizIncorrectAnswer>, IQuizIncorrectAnswerRepository
    {
        public QuizIncorrectAnswerRepository(QuizContext context) : base(context) { }
        public async Task<bool> DoesPairExist(int quizId, int optionId)
        {
            return _context.QuizIncorrectAnswers.Where(qia => qia.QuizId == quizId).Any(qia => qia.IncorrectAnswerId == optionId);
        }
    }
}

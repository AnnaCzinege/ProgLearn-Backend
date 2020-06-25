using Microsoft.EntityFrameworkCore;
using QuizManager.Database.DataAccess;
using QuizManager.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Database.Repositories.SQL
{
    public class QuizRepository : GenericRepository<Models.Quiz>, IQuizRepository
    {
        public QuizRepository(QuizContext context) : base(context) { }

        public async Task<List<string>> GetAllQuestions()
        {
            return await _context.Quizzes.Select(q => q.Question).ToListAsync();
        }
        public async Task<int> GetIdByQuestion(string question)
        {
            Models.Quiz quiz = await _context.Quizzes.FirstAsync(q => q.Question == question);
            return quiz.Id;
        }

        public async Task<Models.Quiz> GetQuizById(int quizId)
        {
            Models.Quiz quiz = await _context.Quizzes.FirstAsync(q => q.Id == quizId);
            return quiz;
        }
    }
}

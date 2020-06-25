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
            return await _context.Quizzes.Include(q => q.IncorrectAnswers).SingleAsync(q => q.Id == quizId);
        }

        public async Task<List<Models.Quiz>> GetQuizzesByCategoryAndDifficulty(string category, string difficulty)
        {
            List<Models.Quiz> result = new List<Models.Quiz>();
            List<Models.Quiz> quizzes = await _context.Quizzes.Where(q => q.Category == category && q.Difficulty == difficulty).ToListAsync();
            foreach (var quiz in quizzes)
            {
                result.Add(await GetQuizById(quiz.Id));
            }
            return result;
        }
    }
}

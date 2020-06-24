﻿using Microsoft.EntityFrameworkCore;
using QuizManager.Database.Models;
using QuizManager.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Database.Repositories.SQL
{
    public class IncorrectAnswerRepository : GenericRepository<IncorrectAnswer>, IIncorrectAnswerRepository
    {
        public IncorrectAnswerRepository(QuizContext context) : base(context) { }
        public async Task<List<string>> GetAllOptions()
        {
            return await _context.IncorrectAnswers.Select(ia => ia.Option).ToListAsync();
        }
    }
}

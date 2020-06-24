using Microsoft.EntityFrameworkCore;
using QuizManager.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Database.Repositories.SQL
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly QuizContext _context;

        public GenericRepository(QuizContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task<TEntity> Find(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
    }
}

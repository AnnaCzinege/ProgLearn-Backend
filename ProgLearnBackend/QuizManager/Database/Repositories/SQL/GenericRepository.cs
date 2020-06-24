using System;
using QuizManager.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Database.Repositories.SQL
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);

        Task Remove(TEntity entity);

        Task Update(TEntity entity);

        Task<List<TEntity>> GetAll();

        Task<TEntity> Find(int id);
    }
}

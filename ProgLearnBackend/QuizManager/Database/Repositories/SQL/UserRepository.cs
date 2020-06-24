using QuizManager.Database.DataAccess;
using QuizManager.Database.Models;
using QuizManager.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Database.Repositories.SQL
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(QuizContext context) : base(context) { }
    }
}

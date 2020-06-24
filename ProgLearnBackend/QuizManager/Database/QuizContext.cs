using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuizManager.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Database
{
    public class QuizContext : IdentityDbContext
    {
        public QuizContext(DbContextOptions options) : base (options) { }
        public DbSet<Models.Quiz> Quizzes { get; set; }
        public DbSet<IncorrectAnswer> IncorrectAnswers { get; set; }
        public DbSet<QuizIncorrectAnswer> QuizIncorrectAnswers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserQuiz> UserQuizzes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<QuizIncorrectAnswer>().HasKey(qia => new { qia.QuizId, qia.IncorrectAnswerId });
            modelBuilder.Entity<UserQuiz>().HasKey(uq => new { uq.QuizId, uq.UserId });

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
    }
}

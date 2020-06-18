using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.DataAccess
{
    public class QuizContext : IdentityDbContext
    {
        public QuizContext(DbContextOptions options) : base(options) { }

        public DbSet<Quiz> Quizes { get; set; }
        public DbSet<IncorrectAnswer> IncorrectAnswers { get; set; }

        public DbSet<QuizIncorrectAnswer> QuizIncorrectAnswers { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserQuiz> QuizSets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserQuiz>().HasKey(uq => new { uq.QuizId, uq.UserId });
            modelBuilder.Entity<QuizIncorrectAnswer>().HasKey(w => new { w.QuizId, w.IncorrectAnswerId });
        }
    }
}

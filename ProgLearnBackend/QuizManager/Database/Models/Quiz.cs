using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Category { get; set; }
        [Required]
        [MaxLength(200)]
        public string Type { get; set; }
        [Required]
        [MaxLength(200)]
        public string Difficulty { get; set; }
        [Required]
        [MaxLength(200)]
        public string Question { get; set; }
        [Required]
        [MaxLength(200)]
        public string CorrectAnswer { get; set; }
        [Required]
        public IList<QuizIncorrectAnswer> IncorrectAnswers { get; set; }
        public IList<UserQuiz> QuizSet { get; set; }
    }
}

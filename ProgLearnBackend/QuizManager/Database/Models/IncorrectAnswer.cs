using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QuizManager.Database.Models
{
    public class IncorrectAnswer
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Option { get; set; }
        public IList<QuizIncorrectAnswer> IncorrectAnswers { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizManager.Database.Models
{
    public class User : IdentityUser
    {
        public IList<UserQuiz> QuizSet { get; set; }
    }
}

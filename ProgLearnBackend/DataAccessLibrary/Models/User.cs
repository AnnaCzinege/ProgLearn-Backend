using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    class User : IdentityUser
    {
        public IList<UserQuiz> QuizSet { get; set; }
    }
}

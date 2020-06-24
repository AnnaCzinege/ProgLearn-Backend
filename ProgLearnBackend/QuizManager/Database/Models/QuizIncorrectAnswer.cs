namespace QuizManager.Database.Models
{
    public class QuizIncorrectAnswer
    {
        public int QuizId { get; set; }
        public int IncorrectAnswerId { get; set; }
    }
}
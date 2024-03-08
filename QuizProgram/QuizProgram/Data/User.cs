namespace QuizProgram.Data
{
   public class User
    {
        public int UserId { get; set; } // Primary key

        // Foreign keys
        public int? StudentId { get; set; }
        public int? ProfessorId { get; set; }

        // Navigation properties
        public Student Student { get; set; }
        public Professor Professor { get; set; }
        public ICollection<Quiz> Quizzes { get; set; }
    }
}

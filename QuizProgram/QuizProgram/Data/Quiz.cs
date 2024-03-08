namespace QuizProgram.Data
{
    public class Quiz
    {
        public int QuizId { get; set; } // Primary key

        // Foreign keys
        public int UserId { get; set; }
        public int CourseId { get; set; }

        public string Answer { get; set; }
        public string Question { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Course Course { get; set; }
    }
}

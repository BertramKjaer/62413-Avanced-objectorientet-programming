namespace QuizProgram.Data
{
    public class Quiz
    {
        public string QuizId { get; set; } // Primary key

        // Foreign keys
        public string UserId { get; set; }
        public string CourseId { get; set; }

        public string Answer { get; set; }
        public string Question { get; set; }

        // Navigation properties
        public ApplicationUser ApplicationUser { get; set; }
        public Course Course { get; set; }
    }
}

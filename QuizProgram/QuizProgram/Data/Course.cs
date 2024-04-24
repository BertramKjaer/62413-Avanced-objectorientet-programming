namespace QuizProgram.Data
{
    public class Course
    {
        public string CourseId { get; set; } // Primary key
        public string Name { get; set; }

        // Navigation properties
        public ICollection<Quiz> Quizzes { get; set; }
    }
}

namespace QuizProgram.Data
{
    public class Course
    {
        public string CourseId { get; set; } // Primary key
        public string Name { get; set; }

        // Navigation properties
        public ICollection<Professor> Professors { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Quiz> Quizzes { get; set; }
    }
}

namespace QuizProgram.Data
{
    public class Course
    {
        public string CourseId { get; set; }// = Guid.NewGuid().ToString(); // Primary key
        public string Name { get; set; } //= string.Empty;

        // Navigation properties
        public ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
    }
}

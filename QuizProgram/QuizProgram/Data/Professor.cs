namespace QuizProgram.Data
{
    public class Professor
    {
        public int ProfessorId { get; set; } // Primary key
        public string Name { get; set; }

        // Foreign key
        public int CourseId { get; set; }

        // Navigation properties
        public Course Course { get; set; }
        public User User { get; set; }
    }
}

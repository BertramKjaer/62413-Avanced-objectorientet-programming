namespace QuizProgram.Data
{
    public class Professor
    {
        public string ProfessorId { get; set; } // Primary key
        public string Name { get; set; }

        // Foreign key
        public string CourseId { get; set; }

        // Navigation properties
        public Course Course { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}

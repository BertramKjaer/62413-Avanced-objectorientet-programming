namespace QuizProgram.Data
{
    public class Student
    {
        public string StudentId { get; set; } = Guid.NewGuid().ToString(); //Primary key
        public string Name { get; set; } = string.Empty;
        public string CourseId { get; set; } = string.Empty;
        public Course Course { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}

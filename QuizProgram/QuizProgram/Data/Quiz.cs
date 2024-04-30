namespace QuizProgram.Data
{
    public class Quiz
    {
        public int QuizId { get; set; }
        public string Title { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }

        // Initialize collections to avoid null references
        public ICollection<Question> Questions { get; set; } = new List<Question>();

        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }

}

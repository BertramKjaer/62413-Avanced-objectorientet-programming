namespace QuizProgram.Data
{
    public class Quiz
    {
        public int QuizId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string QuestionsJson { get; set; } = string.Empty;

        // Added foreign key and navigation properties
        public string CourseId { get; set; } = string.Empty; // Should be string to match Course's ID
        public Course Course { get; set; }
        public string UserId { get; set; } = string.Empty; // Should be string to match ApplicationUser's ID
        public ApplicationUser ApplicationUser { get; set; }
    }
}

public class Question
{
    public string QuizQuestion { get; set; }
    public string CorrectAnswer { get; set; }
    public List<string> IncorrectAnswers { get; set; } = new List<string>();
}
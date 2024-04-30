namespace QuizProgram.Data
{
    public class Quiz
    {
        public int QuizId { get; set; }
        public string Title { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }
        public ICollection<Question> Questions { get; set; }

        // Add a foreign key for ApplicationUser
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }


    public class Question
    {
        public int QuestionId { get; set; }
        public string QuizQuestion { get; set; }
        public string CorrectAnswer { get; set; }
        public string IncorrectAnswer1 { get; set; }
        public string IncorrectAnswer2 { get; set; }
        public string IncorrectAnswer3 { get; set; }

        // Foreign key to Quiz
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
    }
}

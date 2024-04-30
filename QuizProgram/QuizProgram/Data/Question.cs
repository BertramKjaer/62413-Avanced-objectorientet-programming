namespace QuizProgram.Data
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string QuizQuestion { get; set; }
        public string CorrectAnswer { get; set; }
        public string IncorrectAnswer1 { get; set; }
        public string IncorrectAnswer2 { get; set; }
        public string IncorrectAnswer3 { get; set; }

        // Explicitly define the foreign key and navigation property
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
    }
}

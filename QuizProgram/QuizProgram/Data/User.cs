using Microsoft.AspNetCore.Identity;

namespace QuizProgram.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string StudentId { get; set; }
        public string ProfessorId { get; set; }
        public Student Student { get; set; }
        public Professor Professor { get; set; }
        public ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
    }
}

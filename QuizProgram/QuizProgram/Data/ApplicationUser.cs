using Microsoft.AspNetCore.Identity;

namespace QuizProgram.Data
{
   public class ApplicationUser : IdentityUser
    {
        // Foreign keys
        public bool IsStudent { get; set; }
        public bool IsProfessor { get; set; }

        // Navigation properties
        public ICollection<Quiz> Quizzes { get; set; }
    }
    public enum UserType
    {
        Student,
        Professor
    }
}

using Microsoft.AspNetCore.Identity;

namespace QuizProgram.Data
{
   public class ApplicationUser : IdentityUser
    {

        // Foreign keys
        public string StudentId { get; set; }
        public string ProfessorId { get; set; }

        // Navigation properties
        public Student Student { get; set; }
        public Professor Professor { get; set; }
        public ICollection<Quiz> Quizzes { get; set; }
    }
}

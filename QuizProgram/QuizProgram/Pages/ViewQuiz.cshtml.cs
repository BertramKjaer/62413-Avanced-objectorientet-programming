using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizProgram.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace QuizProgram.Pages
{
    [Authorize(Roles = "Professor")]
    public class ViewQuizModel : PageModel
    {
        private readonly QuizProgramContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public List<Quiz> Quizzes { get; set; }

        public ViewQuizModel(QuizProgramContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            Quizzes = await _context.Quizzes
                                    .Include(q => q.Course)
                                    .Include(q => q.ApplicationUser)
                                    .Where(q => q.UserId == currentUser.Id)
                                    .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz != null)
            {
                _context.Quizzes.Remove(quiz);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Quiz deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Quiz not found!";
            }
            return RedirectToPage();
        }
    }
}
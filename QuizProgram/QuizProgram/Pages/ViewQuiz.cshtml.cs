using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using QuizProgram.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace QuizProgram.Pages
{
    [Authorize(Roles = "Professor")]
    public class ViewQuizModel : PageModel
    {
        private readonly QuizProgramContext _context;
        private readonly ILogger<ViewQuizModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public List<Quiz> Quizzes { get; set; }

        public ViewQuizModel(QuizProgramContext context, ILogger<ViewQuizModel> logger, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
            {
                Quizzes = await _context.Quizzes
                                        .Include(q => q.Course)
                                        .Include(q => q.ApplicationUser)
                                        .Where(q => q.UserId == currentUser.Id)
                                        .ToListAsync();
            }
        }
    }
}

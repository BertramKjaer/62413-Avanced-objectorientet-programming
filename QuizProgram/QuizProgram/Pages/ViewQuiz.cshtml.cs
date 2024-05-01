using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using QuizProgram.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace QuizProgram.Pages
{
    [Authorize(Roles = "Professor")]
    public class ViewQuizModel : PageModel
    {
        private readonly QuizProgramContext _context;
        private readonly ILogger<ViewQuizModel> _logger;

        public List<Quiz> Quizzes { get; set; }
        public List<ApplicationUser> Users { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SelectedUserId { get; set; }

        public ViewQuizModel(QuizProgramContext context, ILogger<ViewQuizModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            Users = await _context.Users.ToListAsync();
            if (!string.IsNullOrEmpty(SelectedUserId))
            {
                LoadQuizzes();
            }
        }

        private void LoadQuizzes()
        {
            Quizzes = _context.Quizzes
                              .Include(q => q.Course)
                              .Include(q => q.ApplicationUser)
                              .Where(q => q.UserId == SelectedUserId)
                              .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Users = await _context.Users.ToListAsync();
            if (!string.IsNullOrEmpty(SelectedUserId))
            {
                LoadQuizzes();
            }
            return Page();
        }
    }
}

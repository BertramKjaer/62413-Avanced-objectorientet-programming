using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizProgram.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using static QuizProgram.Pages.CreateQuizModel;
using QuizProgram.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace QuizProgram.Pages
{
        public class EditQuizModel : PageModel
    {
        private readonly QuizProgramContext _context;
        public Quiz Quiz { get; set; }

        [BindProperty]
        public QuizInputModel Input { get; set; } // Reuse or create a specific ViewModel for editing

        public EditQuizModel(QuizProgramContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Quiz = await _context.Quizzes
        .Include(q => q.Questions)
        .FirstOrDefaultAsync(q => q.QuizId == id);

            if (Quiz == null)
            {
                return NotFound();
            }

            Input = new QuizInputModel
            {
                Title = Quiz.Title,
                Questions = Quiz.Questions.Select(q => new QuestionInputModel
                {
                    QuizQuestion = q.QuizQuestion,
                    CorrectAnswer = q.CorrectAnswer,
                    IncorrectAnswer1 = q.IncorrectAnswer1,
                    IncorrectAnswer2 = q.IncorrectAnswer2,
                    IncorrectAnswer3 = q.IncorrectAnswer3
                }).ToList()
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Handle updates here
            return RedirectToPage("./ViewQuizzes");
        }
    }
}
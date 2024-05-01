using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizProgram.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace QuizProgram.Pages
{
    [Authorize(Roles = "Professor")]
    public class CreateQuizModel : PageModel
    {
        private readonly QuizProgramContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateQuizModel(QuizProgramContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public QuizInputModel Input { get; set; }

        public async Task OnGetAsync()
        {
            Input = new QuizInputModel
            {
                Questions = new List<QuestionInputModel> { new QuestionInputModel() }
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                ModelState.AddModelError("", "Problem retrieving user information.");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var courseExists = await _context.Courses.FindAsync(Input.CourseId) != null;

            if (!courseExists)
            {
                ModelState.AddModelError("Input.CourseId", "Course does not exist.");
                return Page();
            }

            var newQuiz = new Quiz
            {
                Title = Input.Title,
                CourseId = Input.CourseId,
                UserId = currentUser.Id,
                Questions = Input.Questions.Select(q => new Question
                {
                    QuizQuestion = q.QuizQuestion,
                    CorrectAnswer = q.CorrectAnswer,
                    IncorrectAnswer1 = q.IncorrectAnswer1,
                    IncorrectAnswer2 = q.IncorrectAnswer2,
                    IncorrectAnswer3 = q.IncorrectAnswer3
                }).ToList()
            };

            try
            {
                _context.Quizzes.Add(newQuiz);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Quiz created successfully!";
                return RedirectToPage("/Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while saving the quiz: {ex.Message}");
                return Page();
            }
        }


        public class QuizInputModel
        {
            [Required]
            [Display(Name = "Quiz Title")]
            public string Title { get; set; }

            [Required]
            [Display(Name = "Course ID")]
            public string CourseId { get; set; }

            public List<QuestionInputModel> Questions { get; set; }
        }

        public class QuestionInputModel
        {
            [Required]
            public string QuizQuestion { get; set; }
            [Required]
            public string CorrectAnswer { get; set; }
            [Required]
            public string IncorrectAnswer1 { get; set; }
            [Required]
            public string IncorrectAnswer2 { get; set; }
            [Required]
            public string IncorrectAnswer3 { get; set; }
        }
    }
}
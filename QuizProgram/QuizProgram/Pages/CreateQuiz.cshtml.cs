using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizProgram.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace QuizProgram.Pages
{
    public class CreateQuizModel : PageModel
    {
        private readonly QuizProgramContext _context;

        public CreateQuizModel(QuizProgramContext context)
        {
            _context = context;
        }

        [BindProperty]
        public QuizInputModel Input { get; set; }

        public class QuizInputModel
        {
            [Required]
            [Display(Name = "Quiz Title")]
            public string Title { get; set; }

            [Required]
            [Display(Name = "Course Id")]
            public string CourseId { get; set; } // In a real application, this should be chosen or provided by the user

            [Required]
            public List<Question> Questions { get; set; } = new List<Question> { new Question() };
        }

        public void OnGet()
        {
            Input = new QuizInputModel
            {
                // Initialize with one question and three empty incorrect answers by default
                Questions = new List<Question> {
            new Question {
                IncorrectAnswers = new List<string> { "", "", "" }
            }
        }
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var newQuiz = new Quiz
            {
                Title = Input.Title,
                CourseId = Input.CourseId, // This should ideally come from the user input or selected course
                QuestionsJson = JsonSerializer.Serialize(Input.Questions),
                // You should fetch the actual UserId of the logged-in user, for example:
                // UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            try
            {
                _context.Quizzes.Add(newQuiz);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Quiz was added successfully!";
                return RedirectToPage("./QuizList");
            }
            catch (Exception ex)
            {
                // Log the exception to the server logs
                // Use a logging framework here, like ILogger
                TempData["ErrorMessage"] = "An error occurred saving the quiz: " + ex.Message;
                return Page();
            }
        }
    }
}

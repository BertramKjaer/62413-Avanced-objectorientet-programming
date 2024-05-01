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
        public List<ApplicationUser> Users { get; set; }  // List to hold users for dropdown

        public async Task OnGetAsync()
        {
            Users = _userManager.Users.ToList();  // Load all users for the dropdown
            Input = new QuizInputModel
            {
                Questions = new List<QuestionInputModel> { new QuestionInputModel() }
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Users = await _userManager.Users.ToListAsync();

            Debug.WriteLine("Received UserId: " + Input.UserId);

            // Check if ModelState is valid
            if (!ModelState.IsValid)
            {
                Users = _userManager.Users.ToList(); // Reload users in case of an error
                return Page();
            }

            // Check if User and Course exist
            var userExists = await _userManager.FindByIdAsync(Input.UserId) != null;
            var courseExists = await _context.Courses.FindAsync(Input.CourseId) != null;

            if (!userExists || !courseExists)
            {
                if (!userExists)
                {
                    ModelState.AddModelError("Input.UserId", "User does not exist.");
                }
                if (!courseExists)
                {
                    ModelState.AddModelError("Input.CourseId", "Course does not exist.");
                }
                return Page();
            }

            // Proceed to create the quiz
            var newQuiz = new Quiz
            {
                Title = Input.Title,
                CourseId = Input.CourseId,
                UserId = Input.UserId,
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
                Users = _userManager.Users.ToList();  // Reload users if an exception is thrown
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

            [Required]
            [Display(Name = "User")]
            public string UserId { get; set; }  // Add User ID field

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
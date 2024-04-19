using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizProgram.Data;


public class Quiz
{
    public string? Title { get; set; }
    public string? TargetAudience { get; set; } // Now nullable
    public List<Question> Questions { get; set; } = new List<Question>();
}

public class Question
{
    public string? QuizQuestion { get; set; } // Now nullable
    public string? CorrectAnswer { get; set; } // Now nullable
    public List<string> IncorrectAnswers { get; set; } = new List<string>() { "", "", "" }; // Initialized with empty strings
}



namespace QuizProgram.Pages
{
    public class CreateQuizModel : PageModel
    {
        [BindProperty]
        public Quiz NewQuiz { get; set; } = new Quiz(); // Make sure NewQuiz is instantiated

        public CreateQuizModel()
        {
            // Constructor can be empty if you're not injecting services
        }

        // OnGet method (only one occurrence)
        public void OnGet()
        {
            if (NewQuiz == null || NewQuiz.Questions.Count == 0)
            {
                NewQuiz = new Quiz();
                NewQuiz.Questions.Add(new Question { IncorrectAnswers = new List<string> { "", "", "" } });
            }
        }

        // OnPost method (only one occurrence)
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Return the page with validation errors
                return Page();
            }

            // TODO: Save the quiz data to a database or in-memory store

            // For now, just add it to a session or TempData to demonstrate functionality
            TempData["Quiz"] = System.Text.Json.JsonSerializer.Serialize(NewQuiz);

            // Redirect to another page after successful creation
            return RedirectToPage("./QuizList"); // Make sure the page exists in your Pages folder
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace QuizProgram.Pages
{
    public class CreateQuiz : PageModel
    {
        [BindProperty]
        public string? QuizTitle { get; set; } // The property is nullable

        // OnGet method (only one occurrence)
        public void OnGet()
        {
            // Initialization code here (if any)
        }

        // OnPost method (only one occurrence)
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Return the page with validation errors
                return Page();
            }

            // Logic to process the quiz creation

            // Redirect to another page after successful creation
            return RedirectToPage("./QuizList"); // Make sure the page exists in your Pages folder
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizProgram.Data;
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
        public Quiz NewQuiz { get; set; }

        // List<Question> only exists in the context of this PageModel for the form binding
        [BindProperty]
        public List<Question> Questions { get; set; }

        public void OnGet()
        {
            Questions = new List<Question> {
                new Question
                {
                    IncorrectAnswers = new List<string> { "", "", ""} //initialize cuz we ballin'
                }
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Serialize the list of questions to JSON
            NewQuiz.QuestionsJson = JsonSerializer.Serialize(Questions);

            // Add the quiz to the database context
            _context.Quizzes.Add(NewQuiz);

            // Save the changes asynchronously
            await _context.SaveChangesAsync();

            // Redirect to the quiz list page or some confirmation page
            return RedirectToPage("./QuizList");
        }
    }

   


    
}

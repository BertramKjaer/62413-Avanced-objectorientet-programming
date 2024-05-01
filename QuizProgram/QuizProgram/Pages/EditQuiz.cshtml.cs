using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizProgram.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace QuizProgram.Pages
{
    public class EditQuizModel : PageModel
    {
        private readonly QuizProgramContext _context;
        public Quiz Quiz { get; set; }

        [BindProperty]
        public QuizInputModel Input { get; set; } // ViewModel for form data

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
                    QuestionId = q.QuestionId, // Ensure you include this property in your QuestionInputModel
                    QuizQuestion = q.QuizQuestion,
                    CorrectAnswer = q.CorrectAnswer,
                    IncorrectAnswer1 = q.IncorrectAnswer1,
                    IncorrectAnswer2 = q.IncorrectAnswer2,
                    IncorrectAnswer3 = q.IncorrectAnswer3
                }).ToList()
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page(); // If model validation fails, re-display the form
            }

            var quizToUpdate = await _context.Quizzes
                .Include(q => q.Questions)
                .FirstOrDefaultAsync(q => q.QuizId == id);

            if (quizToUpdate == null)
            {
                return NotFound($"Unable to load quiz with ID '{id}'.");
            }

            quizToUpdate.Title = Input.Title;

            // Track existing questions by their ID
            var existingQuestions = quizToUpdate.Questions.ToDictionary(q => q.QuestionId);

            // Clear the questions to repopulate them
            quizToUpdate.Questions.Clear();

            foreach (var inputQuestion in Input.Questions)
            {
                Question question;
                if (inputQuestion.QuestionId == 0 || !existingQuestions.TryGetValue(inputQuestion.QuestionId, out question))
                {
                    // New question
                    question = new Question();
                }
                // Update question details
                question.QuizQuestion = inputQuestion.QuizQuestion;
                question.CorrectAnswer = inputQuestion.CorrectAnswer;
                question.IncorrectAnswer1 = inputQuestion.IncorrectAnswer1;
                question.IncorrectAnswer2 = inputQuestion.IncorrectAnswer2;
                question.IncorrectAnswer3 = inputQuestion.IncorrectAnswer3;
                quizToUpdate.Questions.Add(question);
            }

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./ViewQuiz");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }




        private bool QuizExists(int id)
        {
            return _context.Quizzes.Any(e => e.QuizId == id);
        }




    }

    public class QuizInputModel
    {
        public string Title { get; set; }
        public List<QuestionInputModel> Questions { get; set; }
    }

    public class QuestionInputModel
    {
        public int QuestionId { get; set; } // Necessary to identify existing questions
        public string QuizQuestion { get; set; }
        public string CorrectAnswer { get; set; }
        public string IncorrectAnswer1 { get; set; }
        public string IncorrectAnswer2 { get; set; }
        public string IncorrectAnswer3 { get; set; }
    }
}

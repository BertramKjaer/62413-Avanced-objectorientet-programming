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
                return Page(); // Validation failed, re-display the form
            }

            var quizToUpdate = await _context.Quizzes
                .Include(q => q.Questions)
                .FirstOrDefaultAsync(q => q.QuizId == id);

            if (quizToUpdate == null)
            {
                return NotFound($"Unable to load quiz with ID '{id}'.");
            }

            // Update quiz title
            quizToUpdate.Title = Input.Title;

            // List of question IDs from the form input
            var inputQuestionIds = Input.Questions.Select(q => q.QuestionId).ToList();

            // Identify questions to be removed
            var questionsToRemove = quizToUpdate.Questions
                .Where(q => !inputQuestionIds.Contains(q.QuestionId))
                .ToList();

            // Remove questions directly from quizToUpdate
            foreach (var question in questionsToRemove)
            {
                quizToUpdate.Questions.Remove(question);
            }

            // Update existing questions and add new ones
            foreach (var inputQuestion in Input.Questions)
            {
                var question = quizToUpdate.Questions.FirstOrDefault(q => q.QuestionId == inputQuestion.QuestionId);
                if (question != null)
                {
                    // Update existing question
                    question.QuizQuestion = inputQuestion.QuizQuestion;
                    question.CorrectAnswer = inputQuestion.CorrectAnswer;
                    question.IncorrectAnswer1 = inputQuestion.IncorrectAnswer1;
                    question.IncorrectAnswer2 = inputQuestion.IncorrectAnswer2;
                    question.IncorrectAnswer3 = inputQuestion.IncorrectAnswer3;
                }
                else
                {
                    // Add new question
                    quizToUpdate.Questions.Add(new Question
                    {
                        QuizQuestion = inputQuestion.QuizQuestion,
                        CorrectAnswer = inputQuestion.CorrectAnswer,
                        IncorrectAnswer1 = inputQuestion.IncorrectAnswer1,
                        IncorrectAnswer2 = inputQuestion.IncorrectAnswer2,
                        IncorrectAnswer3 = inputQuestion.IncorrectAnswer3
                    });
                }
            }

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./ViewQuiz"); // Redirect after successful update
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

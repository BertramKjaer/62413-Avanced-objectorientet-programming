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
                return Page(); // Return if there are validation errors
            }

            var quizToUpdate = await _context.Quizzes
                .Include(q => q.Questions)
                .FirstOrDefaultAsync(q => q.QuizId == id);

            if (quizToUpdate == null)
            {
                return NotFound($"Unable to load quiz with ID '{id}'.");
            }

            quizToUpdate.Title = Input.Title; // Update the title

            var existingQuestionIds = quizToUpdate.Questions.Select(q => q.QuestionId).ToList();

            // Update questions or add new ones
            foreach (var questionModel in Input.Questions)
            {
                var question = quizToUpdate.Questions.FirstOrDefault(q => q.QuestionId == questionModel.QuestionId);
                if (question != null)
                {
                    // Update existing question
                    question.QuizQuestion = questionModel.QuizQuestion;
                    question.CorrectAnswer = questionModel.CorrectAnswer;
                    question.IncorrectAnswer1 = questionModel.IncorrectAnswer1;
                    question.IncorrectAnswer2 = questionModel.IncorrectAnswer2;
                    question.IncorrectAnswer3 = questionModel.IncorrectAnswer3;
                }
                else if (!existingQuestionIds.Contains(questionModel.QuestionId))
                {
                    // Add new question if it doesn't exist
                    quizToUpdate.Questions.Add(new Question
                    {
                        QuizQuestion = questionModel.QuizQuestion,
                        CorrectAnswer = questionModel.CorrectAnswer,
                        IncorrectAnswer1 = questionModel.IncorrectAnswer1,
                        IncorrectAnswer2 = questionModel.IncorrectAnswer2,
                        IncorrectAnswer3 = questionModel.IncorrectAnswer3
                    });
                }
            }

            // Optionally remove deleted questions
            // You might need to adjust your model to flag questions for deletion

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./ViewQuiz"); // Make sure this is the correct redirect
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Quizzes.Any(q => q.QuizId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
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

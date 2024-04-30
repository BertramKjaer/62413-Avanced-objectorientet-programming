using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizProgram.Data;
using System.Threading.Tasks;

namespace QuizProgram.Pages
{
    public class SomePageModel : PageModel
    {
        private readonly CourseService _courseService;

        public SomePageModel(CourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task OnGet()
        {
            await _courseService.AddCourseAsync();
        }
    }
}

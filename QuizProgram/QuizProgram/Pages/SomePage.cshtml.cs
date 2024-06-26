using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizProgram.Data;
using System.Threading.Tasks;

namespace QuizProgram.Pages
{
    [Authorize(Roles = "Professor")]
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

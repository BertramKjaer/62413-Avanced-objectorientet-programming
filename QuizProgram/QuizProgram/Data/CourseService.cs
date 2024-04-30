using QuizProgram.Data;
using System.Threading.Tasks;

namespace QuizProgram.Data
{
    public class CourseService
    {
        private readonly QuizProgramContext _context;

        public CourseService(QuizProgramContext context)
        {
            _context = context;
        }

        public async Task AddCourseAsync()
        {
            var existingCourse = await _context.Courses.FindAsync("62413");
            if (existingCourse == null)
            {
                var newCourse = new Course
                {
                    CourseId = "62413",
                    Name = "Advanced object oriented programming using C# and .NET"
                };

                _context.Courses.Add(newCourse);
                await _context.SaveChangesAsync();
            }
        }
    }
}

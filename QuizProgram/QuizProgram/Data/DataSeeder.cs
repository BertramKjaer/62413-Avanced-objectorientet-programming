using QuizProgram.Data;

public static class DataSeeder
{
    public static void SeedData(QuizProgramContext context)
    {
        context.Database.EnsureCreated();  // Make sure the database is created

        // Seed Courses
        if (!context.Courses.Any())
        {
            context.Courses.Add(new Course
            {
                CourseId = "62413",
                Name = "Advanced object-oriented programming using C# and .NET"
            });

            context.SaveChanges();
        }
    }
}

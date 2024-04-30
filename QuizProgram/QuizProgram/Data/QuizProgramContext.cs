using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace QuizProgram.Data { 

    public class QuizProgramContext : IdentityDbContext<ApplicationUser>
    {
        public QuizProgramContext(DbContextOptions<QuizProgramContext> options)
            : base(options)
        {
        }

        // Add DbSet properties for entities
        public DbSet<Course> Courses { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This needs to be called first
            modelBuilder.Entity<Course>().HasData(new Course
            {
                CourseId = "62413",
                Name = "Advanced object oriented programming using C# and .NET"
            });

            modelBuilder.Entity<Course>()
                    .HasKey(c => c.CourseId); // Explicitly define CourseId as the primary key if not using convention

            modelBuilder.Entity<Course>()
                    .HasMany(c => c.Quizzes)
                    .WithOne(q => q.Course)
                    .HasForeignKey(q => q.CourseId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Quizzes)
                .WithOne(q => q.ApplicationUser)
                .HasForeignKey(q => q.UserId);

            modelBuilder.Entity<Quiz>()
                .HasMany(q => q.Questions)
                .WithOne(q => q.Quiz)
                .HasForeignKey(q => q.QuizId);
        }
    }

}
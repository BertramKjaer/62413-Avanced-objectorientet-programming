using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace QuizProgram.Data;

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


        modelBuilder.Entity<Quiz>()
        .HasMany(q => q.Questions)
        .WithOne(q => q.Quiz)
        .HasForeignKey(q => q.QuizId);

        // Configure one-to-one relationships
        // Currently there are none

        // Configure one-to-many relationships
        modelBuilder.Entity<Course>()
            .HasMany(c => c.Quizzes)
            .WithOne(q => q.Course)
            .HasForeignKey(q => q.CourseId);
        
        modelBuilder.Entity<ApplicationUser>()
            .HasMany(u => u.Quizzes)
            .WithOne(q => q.ApplicationUser)
            .HasForeignKey(q => q.UserId);
    }
}

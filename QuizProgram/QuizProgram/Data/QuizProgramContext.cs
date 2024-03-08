using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace QuizProgram.Data;

public class QuizProgramContext : IdentityDbContext<ApplicationUser>
{
    public QuizProgramContext(DbContextOptions<QuizProgramContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure one-to-one relationships
        modelBuilder.Entity<User>()
            .HasOne(u => u.Student)
            .WithOne(s => s.User)
            .HasForeignKey<User>(u => u.StudentId);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Professor)
            .WithOne(p => p.User)
            .HasForeignKey<User>(u => u.ProfessorId);

        // Configure one-to-many relationships
        modelBuilder.Entity<Course>()
            .HasMany(c => c.Professors)
            .WithOne(p => p.Course)
            .HasForeignKey(p => p.CourseId);

        modelBuilder.Entity<Course>()
            .HasMany(c => c.Students)
            .WithOne(s => s.Course)
            .HasForeignKey(s => s.CourseId);

        modelBuilder.Entity<Course>()
            .HasMany(c => c.Quizzes)
            .WithOne(q => q.Course)
            .HasForeignKey(q => q.CourseId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Quizzes)
            .WithOne(q => q.User)
            .HasForeignKey(q => q.UserId);

        // Additional Fluent API configurations as needed
    }
}

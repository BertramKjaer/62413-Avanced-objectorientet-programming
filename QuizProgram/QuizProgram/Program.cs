using Microsoft.EntityFrameworkCore;
using QuizProgram.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Database services
builder.Services.AddDbContext<QuizProgram.Data.QuizProgramContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("QuizProgramContext") ??
        "Data Source=quiz.db")); // Connection string from configuration or default

// Login services
builder.Services.AddDefaultIdentity<ApplicationUser>()
    .AddEntityFrameworkStores<QuizProgramContext>();

builder.Services.AddScoped<CourseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Login authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

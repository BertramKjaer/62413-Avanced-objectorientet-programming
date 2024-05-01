using Microsoft.EntityFrameworkCore;
using QuizProgram.Data;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Database services
builder.Services.AddDbContext<QuizProgramContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("QuizProgramContext") ??
        "Data Source=quiz.db"));

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

// Redirect root to login page if not signed in else redirect to home
app.MapGet("/", async context =>
{
    // Check if the user is authenticated
    if (context.User.Identity.IsAuthenticated)
    {
        context.Response.Redirect("/Home");
    }
    else
    {
        context.Response.Redirect("/Identity/Account/Login");
    }
    await Task.CompletedTask;
});

app.Run();

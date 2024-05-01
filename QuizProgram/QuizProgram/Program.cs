using Microsoft.EntityFrameworkCore;
using QuizProgram.Data;
using Microsoft.AspNetCore.Identity;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Database services
builder.Services.AddDbContext<QuizProgramContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("QuizProgramContext") ??
        "Data Source=quiz.db"));

// Login services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<QuizProgramContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<CourseService>();

var app = builder.Build();

// Obtain the role manager and user manager services
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    await IdentityDataInitializer.SeedData(userManager, roleManager);
}

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

public static class IdentityDataInitializer
{
    public static async Task SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        await EnsureRoleAsync(roleManager, "Professor");
        await EnsureRoleAsync(roleManager, "Student");
    }

    private static async Task EnsureRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
    {
        var roleExists = await roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}
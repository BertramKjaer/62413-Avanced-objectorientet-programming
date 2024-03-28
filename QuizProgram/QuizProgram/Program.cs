using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("QuizProgramContextConnection") ?? throw new InvalidOperationException("Connection string 'QuizProgramContextConnection' not found.");

builder.Services.AddDbContext<QuizProgramContext>(options => options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<QuizProgramContext>();

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<QuizProgram.Data.QuizProgramContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("QuizProgramContext") ??
        "Data Source=quiz.db")); // Connection string from configuration or default

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

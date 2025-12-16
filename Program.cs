using EvalApi.Src.Core.Middlewares;
using EvalApi.Src.Core.Repositories;
using EvalApi.Src.Core.Repositories.Implementations;
using EvalApi.Src.Core.Repositories.Interfaces;
using EvalApi.Src.Core.Services.Implementations;
using EvalApi.Src.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DB folder must exist before opening SQLite file
Directory.CreateDirectory("Data");

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=Data/database.db"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("Localhost3000", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Repos
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();

var app = builder.Build();

// Apply migrations at startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Pipeline order (as required)
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseCors("Localhost3000");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();

using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Serilog.Events;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/app-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/register", async (AppDbContext db, [FromBody] UserRegistrationDto userDto, IPasswordHasher<User> passwordHasher, ILogger<Program> logger) =>
{
    // Check if the role is valid
    if (!Enum.IsDefined(typeof(UserRole), userDto.Role))
    {
        logger.LogWarning("Invalid role attempted: {Role}", userDto.Role);
        return Results.BadRequest("Invalid role specified");
    }

    // Check if username already exists
    if (await db.Users.AnyAsync(u => u.Username == userDto.Username))
    {
        logger.LogInformation("Registration attempt with existing username: {Username}", userDto.Username);
        return Results.BadRequest("Username already exists");
    }

    var user = new User
    {
        Username = userDto.Username,
        Role = userDto.Role,
        PasswordHash = string.Empty
    };

    user.PasswordHash = passwordHasher.HashPassword(user, userDto.Password);

    try
    {
        db.Users.Add(user);
        await db.SaveChangesAsync();

        logger.LogInformation("User registered successfully: {UserId}", user.Id);

        var responseDto = new UserResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            Role = user.Role
        };

        return Results.Created($"/users/{user.Id}", responseDto);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error occurred while registering user");
        return Results.StatusCode(500);
    }
})
.WithName("RegisterUser")
.WithOpenApi();

app.MapPost("/login", async (AppDbContext db, [FromBody] UserLoginDto loginDto, IPasswordHasher<User> passwordHasher, ILogger<Program> logger) =>
{
    var user = await db.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username);

    if (user == null)
    {
        logger.LogWarning("Login attempt with non-existent username: {Username}", loginDto.Username);
        return Results.Unauthorized();
    }

    var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);

    if (passwordVerificationResult == PasswordVerificationResult.Failed)
    {
        logger.LogWarning("Failed login attempt for user: {UserId}", user.Id);
        return Results.Unauthorized();
    }

    logger.LogInformation("User logged in successfully: {UserId}", user.Id);

    var responseDto = new UserResponseDto
    {
        Id = user.Id,
        Username = user.Username,
        Role = user.Role
    };

    return Results.Ok(responseDto);
})
.WithName("LoginUser")
.WithOpenApi();

app.Run();

// Ensure to flush and close the log at the end of the program
Log.CloseAndFlush();

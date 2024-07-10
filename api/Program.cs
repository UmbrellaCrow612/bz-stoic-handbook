using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

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

app.Run();

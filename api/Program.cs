using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);


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

app.MapPost("/register", async (AppDbContext db, [FromBody] UserRegistrationDto userDto, IPasswordHasher<User> passwordHasher) =>
{
    if (await db.Users.AnyAsync(u => u.Username == userDto.Username))
    {
        return Results.BadRequest("Username already exists");
    }

    var user = new User
    {
        PasswordHash = string.Empty,
        Username = userDto.Username,
        Role = userDto.Role
    };

    user.PasswordHash = passwordHasher.HashPassword(user, userDto.Password);

    db.Users.Add(user);
    await db.SaveChangesAsync();

    return Results.Created($"/users/{user.Id}", new { user.Username, user.Role });
})
.WithName("RegisterUser")
.WithOpenApi();

app.Run();

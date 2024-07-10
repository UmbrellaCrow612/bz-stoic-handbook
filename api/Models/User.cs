using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public required UserRole Role { get; set; }
    }

    public class UserRegistrationDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public required string Username { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public required string Password { get; set; }

        [Required]
        public required UserRole Role { get; set; }
    }

    public class UserResponseDto
    {
        public required string Id { get; set; }
        public required string Username { get; set; }
        public UserRole Role { get; set; }
    }

    public class UserLoginDto
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Password { get; set; }
    }

    public enum UserRole
    {
        Admin,
        User
    }
}

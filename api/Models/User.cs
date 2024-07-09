using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public  required string Role { get; set; }

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
        [StringLength(20)]
        public required string Role { get; set; }
    }
}

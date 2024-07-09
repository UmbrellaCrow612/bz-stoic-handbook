﻿namespace api.Models
{
    public class User
    {
        public required string Id { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public  required string Role { get; set; }

        public User()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}

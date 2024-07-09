namespace api.Models
{
    public class Admin
    {
        public required string Id { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }

        public Admin()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}

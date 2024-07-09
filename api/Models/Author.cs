namespace api.Models
{
    public class Author
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Biography { get; set; }
        public required DateTime BirthDate { get; set; }
        public required DateTime DeathDate { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public List<Book> Books { get; set; } = [];
        public List<Quote> Quotes { get; set; } = [];

        public Author()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}

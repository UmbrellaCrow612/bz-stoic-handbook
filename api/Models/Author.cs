namespace api.Models
{
    public class Author
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Biography { get; set; } = string.Empty;
        public required DateTime BirthDate { get; set; }
        public string ProfilePictureUrl { get; set; } = string.Empty;
        public List<Book> Books { get; set; } = [];
        public List<Quote> Quotes { get; set; } = [];

    }
}

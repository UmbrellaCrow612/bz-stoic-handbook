namespace api.Models
{
    public class Book
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public required Author Author { get; set; }
        public required string Description { get; set; }
        public required int PublicationYear { get; set; }
        public string? BookUrl { get; set; }

        public Book()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}

namespace api.Models
{
    public class Book
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public required Author Author { get; set; }
        public string Description { get; set; } = string.Empty;
        public required int PublicationYear { get; set; }
        public string? BookUrl { get; set; } = string.Empty;

    }
}

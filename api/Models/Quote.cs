namespace api.Models
{
    public class Quote
    {
        public required string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Content { get; set; }
        public required Author Author { get; set; }

    }
}

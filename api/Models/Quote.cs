namespace api.Models
{
    public class Quote
    {
        public required string Id { get; set; }
        public required string Content { get; set; }
        public required Author Author { get; set; }

        public Quote()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}

namespace api.Models
{
    public class Document
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime EditedAt { get; set; }
        public List<Tag> Tags { get; set; }
        public Folder? Folder { get; set; }

        public Document()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.UtcNow;
            EditedAt = DateTime.UtcNow;
            Tags = [];
        }
    }
}

namespace api.Models
{
    public class Document
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime EditedAt { get; set; } = DateTime.UtcNow;
        public List<Tag> Tags { get; set; } = [];
        public Folder? Folder { get; set; }
    }
}

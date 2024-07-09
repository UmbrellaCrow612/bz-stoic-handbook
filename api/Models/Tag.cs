namespace api.Models
{
    public class Tag
    {
        public required string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Name { get; set; }
        public List<Document> Documents { get; set; } = [];
     
    }
}

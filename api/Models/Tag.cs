namespace api.Models
{
    public class Tag
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public List<Document> Documents { get; set; }

        public Tag()
        {
            Id = Guid.NewGuid().ToString();
            Documents = [];
        }
    }
}

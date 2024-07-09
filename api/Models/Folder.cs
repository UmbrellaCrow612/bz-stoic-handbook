namespace api.Models
{
    public class Folder
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public List<Document> Documents { get; set; }

        public Folder()
        {
            Id = Guid.NewGuid().ToString();
            Documents = [];
        }
    }
}

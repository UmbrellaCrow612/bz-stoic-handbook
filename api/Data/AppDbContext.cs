using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Document> Documents { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Book> Books { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure many-to-many relationship between Document and Tag
            modelBuilder.Entity<Document>()
                .HasMany(d => d.Tags)
                .WithMany(t => t.Documents)
                .UsingEntity(j => j.ToTable("DocumentTags"));

            // Configure one-to-many relationship between Folder and Document
            modelBuilder.Entity<Folder>()
                .HasMany(f => f.Documents)
                .WithOne(d => d.Folder)
                .HasForeignKey("FolderId");

            // Configure one-to-many relationship between Author and Book
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey("AuthorId");

            // Configure one-to-many relationship between Author and Quote
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Quotes)
                .WithOne(q => q.Author)
                .HasForeignKey("AuthorId");
        }
    }
}

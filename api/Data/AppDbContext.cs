using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Document> Documents { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Admin> Admins { get; set; }

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
        }
    }
}

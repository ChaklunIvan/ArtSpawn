using ArtSpawn.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArtSpawn.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) 
            : base(options) { }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductCategory>()
                .HasKey(pc => new { pc.ProductId, pc.CategoryId });
            builder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);
            builder.Entity<ProductCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId);
        }
    }
}

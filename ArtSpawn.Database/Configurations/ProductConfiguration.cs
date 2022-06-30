using ArtSpawn.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtSpawn.Database.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("product")
                .HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(p => p.Title)
                .HasColumnName("product_title")
                .IsRequired()
                .HasMaxLength(32);

            builder.Property(p => p.Description)
                .HasColumnName("product_description")
                .HasMaxLength(200);

            builder.Property(p => p.Price)
                .HasPrecision(7, 2)
                .HasColumnName("product_price")
                .IsRequired();

            builder.Property(p => p.File)
                .HasColumnName("file");

            builder.Property(p => p.ArtistId)
                .HasColumnName("artist_id");

            builder.Property(p => p.CategoryId)
                .HasColumnName("category_id");

            builder.HasOne(a => a.Artist)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.ArtistId);

            builder.HasOne(c => c.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId);
        }
    }
}

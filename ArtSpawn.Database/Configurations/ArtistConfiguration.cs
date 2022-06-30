using ArtSpawn.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ArtSpawn.Database.Configurations
{
    public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.ToTable("artist")
                .HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(a => a.Name)
                .HasColumnName("artist_name")
                .IsRequired()
                .HasMaxLength(32);
            builder.HasIndex(a => a.Name).IsUnique();

            builder.Property(a => a.About)
                .HasColumnName("about_artist")
                .HasMaxLength(200);

            builder.Property(a => a.Image)
                .HasColumnName("profile_image");

            builder.HasMany(p => p.Products)
                .WithOne(a => a.Artist);
        }
    }
}

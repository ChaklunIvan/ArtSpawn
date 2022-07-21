using ArtSpawn.Models.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtSpawn.Database.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name = RoleCostants.Artist,
                    NormalizedName = RoleCostants.NormalizedArtist
                },
                new IdentityRole
                {
                    Name = RoleCostants.Admin,
                    NormalizedName = RoleCostants.NormalizedAdmin
                });
                
        }
    }
}

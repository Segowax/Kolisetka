using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kolisetka.Identity.DataSeed
{
    public class RoleSeed : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "8b693986-0d12-487f-9fde-e0e983e2f51c",
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Id = "c619043f-2923-4180-95fc-1104ed3ddc3e",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                });
        }
    }
}

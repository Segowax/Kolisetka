using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kolisetka.Identity.DataSeed
{
    public class UserToRoleSeed : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "8b693986-0d12-487f-9fde-e0e983e2f51c",
                    UserId = "0be5b79b-d566-4fb1-b9f7-9aa1115d889b"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "c619043f-2923-4180-95fc-1104ed3ddc3e",
                    UserId = "cfd8e76a-aa81-41b3-8623-a6882006a126"
                });
        }
    }
}

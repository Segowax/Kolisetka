using Kolisetka.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kolisetka.Identity.DataSeed
{
    public class UserSeed : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var hasher = new PasswordHasher<User>();
            var user1 = new User
            {
                Id = "0be5b79b-d566-4fb1-b9f7-9aa1115d889b",
                Email = "user@localhost.com",
                NormalizedEmail = "USER@LOCALHOST.COM",
                FirstName = "System",
                LastName = "User",
                UserName = "user@localhost.com",
                NormalizedUserName = "USER@LOCALHOST.COM",
                EmailConfirmed = true
            };
            var user2 = new User
            {
                Id = "cfd8e76a-aa81-41b3-8623-a6882006a126",
                Email = "admin@localhost.com",
                NormalizedEmail = "ADMIN@LOCALHOST.COM",
                FirstName = "System",
                LastName = "Admin",
                UserName = "admin@localhost.com",
                NormalizedUserName = "ADMIN@LOCALHOST.COM",
                EmailConfirmed = true
            };
            user1.PasswordHash = hasher.HashPassword(user1, "kotyToBumBum!2");
            user2.PasswordHash = hasher.HashPassword(user2, "kotyToBumBum!2");

            builder.HasData(user1, user2);
        }
    }
}

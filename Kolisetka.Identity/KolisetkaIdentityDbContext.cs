using Kolisetka.Domain.Models;
using Kolisetka.Identity.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kolisetka.Identity
{
    public class KolisetkaIdentityDbContext : IdentityDbContext<User>
    {
        public KolisetkaIdentityDbContext(DbContextOptions<KolisetkaIdentityDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}

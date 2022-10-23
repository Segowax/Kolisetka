using Kolisetka.Domain.Models;
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

            // Apply all configurations and data seeding!
            builder.ApplyConfigurationsFromAssembly(typeof(KolisetkaIdentityDbContext).Assembly);
        }
    }
}

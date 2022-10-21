using Kolisetka.Domain;
using Kolisetka.Domain.Common;
using Kolisetka.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kolisetka.Persistence
{
    public class KolisetkaDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public KolisetkaDbContext(DbContextOptions<KolisetkaDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply all configurations and data seeding! I wonder whether I can assembly each separately.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(KolisetkaDbContext).Assembly);
        }

        public override int SaveChanges()
        {
            foreach (var change in ChangeTracker.Entries<Base>())
            {
                change.Entity.DateUpdated = DateTime.UtcNow;

                if (change.State == EntityState.Added)
                    change.Entity.DateCreated = DateTime.UtcNow;
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var change in ChangeTracker.Entries<Base>())
            {
                change.Entity.DateUpdated = DateTime.UtcNow;

                if (change.State == EntityState.Added)
                    change.Entity.DateCreated = DateTime.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

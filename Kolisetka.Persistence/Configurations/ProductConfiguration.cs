using Kolisetka.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kolisetka.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(prop => prop.Category).IsRequired();
            builder.Property(prop => prop.Description).IsRequired().HasMaxLength(1000);
            builder.Property(prop => prop.Name).IsRequired().HasMaxLength(100);
            builder.Property(prop => prop.Price).IsRequired().HasPrecision(7, 2);
        }
    }
}

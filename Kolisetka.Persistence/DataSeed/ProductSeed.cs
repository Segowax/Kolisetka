using Kolisetka.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Kolisetka.Persistence.DataSeed
{
    public class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product
                {
                    Id = 1,
                    Category = Category.AlkoholicDrink,
                    DateCreated = new DateTime(2022, 6, 1),
                    DateUpdated = new DateTime(2022, 6, 1),
                    Description = "Piwo 500 ml, czeskie z nalewaka.",
                    Name = "Holba",
                    Price = 6.00m,
                },
                new Product
                {
                    Id = 2,
                    Category = Category.Food,
                    DateCreated = new DateTime(2022, 6, 1),
                    DateUpdated = new DateTime(2022, 6, 1),
                    Description = "Najsmaczniejsza golonka na całym Kozanownie!",
                    Name = "Golonka",
                    Price = 15.00m
                },
                new Product
                {
                    Id = 3,
                    Category = Category.SoftDrink,
                    DateCreated = new DateTime(2022, 6, 1),
                    DateUpdated = new DateTime(2022, 6, 1),
                    Description = "Woda 200 ml, w szklanej butelce.",
                    Name = "Kinga",
                    Price = 2.50m
                });
        }
    }
}

using Kolisetka.Application.Contracts.Persistence;
using Kolisetka.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kolisetka.Application.UnitTests.Mocks
{
    public static class MockProductRepository
    {
        public static Mock<IProductRepository> GetProductsRepository()
        {
            var products = new List<Product>
            {
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
                }
            };
            var mockRepo = new Mock<IProductRepository>();

            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(products);
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Product>())).Returns((Product product) =>
            {
                products.Add(product);

                return Task.CompletedTask;
            });

            return mockRepo;
        }
    }
}

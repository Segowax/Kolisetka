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

            mockRepo.Setup(r => r.GetAsync(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                return products.Find(product => product.Id == id);
            });
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(products);
            mockRepo.Setup(r => r.IsExist(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                return products.Find(product => product.Id == id) != null;
            });
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Product>())).Returns((Product product) =>
            {
                product.DateCreated = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, 0);
                product.DateUpdated = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, 0);
                products.Add(product);

                return Task.CompletedTask;
            });
            mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Product>())).Returns((Product updatedProduct) =>
            {
                foreach (var product in products)
                {
                    if (product.Id == updatedProduct.Id)
                    {
                        product.Category = updatedProduct.Category;
                        product.DateCreated = updatedProduct.DateCreated;
                        product.DateUpdated = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, 0);
                        product.Description = updatedProduct.Description;
                        product.Name = updatedProduct.Name;
                        product.Price = updatedProduct.Price;
                    }
                }

                return Task.CompletedTask;
            });
            mockRepo.Setup(r => r.DeleteAsync(It.IsAny<Product>())).Returns((Product deletedProduct) =>
            {
                products.Remove(deletedProduct);

                return Task.CompletedTask;
            });

            return mockRepo;
        }
    }
}

using AutoMapper;
using Kolisetka.Application.Contracts.Persistence;
using Kolisetka.Application.DTOs.DtoProduct;
using Kolisetka.Application.Features.Products.Handlers.Commands;
using Kolisetka.Application.Features.Products.Requests.Commands;
using Kolisetka.Application.Profiles;
using Kolisetka.Application.UnitTests.Mocks;
using Kolisetka.Domain;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Kolisetka.Application.UnitTests.Products.Commands
{
    public class UpdateProductCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly UpdateProductCommandHandler _handler;
        private readonly ProductUpdateDto _productDto;

        public UpdateProductCommandHandlerTest()
        {
            _mockRepo = MockProductRepository.GetProductsRepository();

            var mapperConfig = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _handler = new UpdateProductCommandHandler(_mockRepo.Object, _mapper);

            _productDto = new ProductUpdateDto
            {
                Id = 2,
                Category = Category.Food,
                Description = "Najsmaczniejsza golonka na całym Kozanownie!",
                Name = "Golonka",
                Price = 10.00m
            };
        }

        [Fact]
        public async Task Valid_Product_Update()
        {
            await _handler.Handle
                (new UpdateProductCommand() { ProductUpdateDto = _productDto }, CancellationToken.None);

            var products = await _mockRepo.Object.GetAllAsync();
            products.Count.ShouldBe(3);

            var updatedProduct = await _mockRepo.Object.GetAsync(2);
            updatedProduct.Category.ShouldBe(Category.Food);
            updatedProduct.DateCreated.ShouldBe(new DateTime(2022, 6, 1));
            updatedProduct.DateUpdated.ShouldBe(new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, 0));
            updatedProduct.Description.ShouldBe("Najsmaczniejsza golonka na całym Kozanownie!");
            updatedProduct.Name.ShouldBe("Golonka");
            updatedProduct.Price.ShouldBe(10.00m);
        }
    }
}

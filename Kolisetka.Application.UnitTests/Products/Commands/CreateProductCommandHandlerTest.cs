using AutoMapper;
using Kolisetka.Application.Contracts.Persistence;
using Kolisetka.Application.DTOs.DtoProduct;
using Kolisetka.Application.Exceptions;
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

using TestProperties = Kolisetka.Application.UnitTests.Properties;
using ApplicationProperties = Kolisetka.Application.Properties;

namespace Kolisetka.Application.UnitTests.Products.Commands
{
    public class CreateProductCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly CreateProductCommandHandler _handler;
        private readonly ProductCreateDto _productDto;

        public CreateProductCommandHandlerTest()
        {
            _mockRepo = MockProductRepository.GetProductsRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _handler = new CreateProductCommandHandler(_mockRepo.Object, _mapper);

            _productDto = new ProductCreateDto
            {
                Category = Category.Food,
                Description = "Na mały głód.",
                Name = "Zapiekanka",
                Price = 10.00m
            };
        }

        [Fact]
        public async Task Add_Valid_Product_Test()
        {
            await _handler.Handle
                (new CreateProductCommand() { ProductCreateDto = _productDto }, CancellationToken.None);

            var products = await _mockRepo.Object.GetAllAsync();
            products.Count.ShouldBe(4);

            var addedProduct = products[^1];
            addedProduct.Category.ShouldBe(Category.Food);
            addedProduct.DateCreated.ShouldBe(new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, 0));
            addedProduct.DateUpdated.ShouldBe(new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, 0));
            addedProduct.Description.ShouldBe("Na mały głód.");
            addedProduct.Name.ShouldBe("Zapiekanka");
            addedProduct.Price.ShouldBe(10.00m);
        }

        [Fact]
        public async Task Add_Product_With_Invalid_Price_Test()
        {
            // invalid precision
            _productDto.Price = 10.002m;
            var ex = await Should.ThrowAsync<ValidationException>
                (async () => await _handler.Handle
                    (new CreateProductCommand() { ProductCreateDto = _productDto }, CancellationToken.None));

            ex.ValidationErrors.Errors.Count.ShouldBe(1);
            ex.ValidationErrors.Errors[0].ShouldBe(ApplicationProperties.Resources.Product_Validator_InvalidPrecision
                .Replace("{PropertyName}", nameof(CreateProductCommand.ProductCreateDto.Price)));

            var products = await _mockRepo.Object.GetAllAsync();
            products.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Add_Product_With_Invalid_Category_Test()
        {
            // invalid enum
            _productDto.Category = (Category)3;
            var ex = await Should.ThrowAsync<ValidationException>
                (async () => await _handler.Handle
                    (new CreateProductCommand() { ProductCreateDto = _productDto }, CancellationToken.None));

            ex.ValidationErrors.Errors[0].ShouldBe(ApplicationProperties.Resources.Product_Validator_InvalidEnum
                .Replace("{PropertyName}", nameof(CreateProductCommand.ProductCreateDto.Category)));

            var products = await _mockRepo.Object.GetAllAsync();
            products.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Add_Product_With_Invalid_Name_Test()
        {
            // null
            _productDto.Name = null;
            var ex = await Should.ThrowAsync<ValidationException>
                (async () => await _handler.Handle
                    (new CreateProductCommand() { ProductCreateDto = _productDto }, CancellationToken.None));

            // too long
            _productDto.Name = TestProperties.Resources.Test_TooLongString_101;
            ex = await Should.ThrowAsync<ValidationException>
                (async () => await _handler.Handle
                    (new CreateProductCommand() { ProductCreateDto = _productDto }, CancellationToken.None));

            var products = await _mockRepo.Object.GetAllAsync();
            products.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Add_Product_With_Invalid_Description_Test()
        {
            // null
            _productDto.Description = null;
            var ex = await Should.ThrowAsync<ValidationException>
                (async () => await _handler.Handle
                    (new CreateProductCommand() { ProductCreateDto = _productDto }, CancellationToken.None));

            // too long
            _productDto.Description = TestProperties.Resources.Test_TooLongString_1001;
            ex = await Should.ThrowAsync<ValidationException>
                (async () => await _handler.Handle
                    (new CreateProductCommand() { ProductCreateDto = _productDto }, CancellationToken.None));

            var products = await _mockRepo.Object.GetAllAsync();
            products.Count.ShouldBe(3);
        }
    }
}

using AutoMapper;
using Kolisetka.Application.Contracts.Persistence;
using Kolisetka.Application.DTOs.DtoProduct;
using Kolisetka.Application.Exceptions;
using Kolisetka.Application.Features.Products.Handlers.Commands;
using Kolisetka.Application.Features.Products.Requests.Commands;
using Kolisetka.Application.Profiles;
using Kolisetka.Application.Responses;
using Kolisetka.Application.UnitTests.Mocks;
using Kolisetka.Domain;
using MediatR;
using Moq;
using Shouldly;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

using ApplicationProperties = Kolisetka.Application.Properties;
using TestProperties = Kolisetka.Application.UnitTests.Properties;

namespace Kolisetka.Application.UnitTests.Products.Commands
{
    public class UpdateProductCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly UpdateProductCommandHandler _handler;
        private readonly ProductUpdateDto _productDto;

        private string MyString;

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
        public async Task Update_Product_With_Valid_Data()
        {
            var response = await _handler.Handle
                (new UpdateProductCommand() { ProductUpdateDto = _productDto }, CancellationToken.None);
            response.ShouldBeOfType<Unit>();

            var products = await _mockRepo.Object.GetAllAsync();
            products.Count.ShouldBe(3);

            var updatedProduct = products.FirstOrDefault(product => product.Id == _productDto.Id);
            updatedProduct.Category.ShouldBe(Category.Food);
            updatedProduct.DateCreated.ShouldBe(new DateTime(2022, 6, 1));
            updatedProduct.DateUpdated.ShouldBe(new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, 0));
            updatedProduct.Description.ShouldBe("Najsmaczniejsza golonka na całym Kozanownie!");
            updatedProduct.Name.ShouldBe("Golonka");
            updatedProduct.Price.ShouldBe(10.00m);
        }

        [Fact]
        public async Task Update_Product_With_Invalid_Id_Test()
        {
            _productDto.Id = 5;
            var ex = await Should.ThrowAsync<ValidationException>
                (async () => await _handler.Handle
                    (new UpdateProductCommand() { ProductUpdateDto = _productDto }, CancellationToken.None));
            ex.ValidationErrors.Errors.Count.ShouldBe(1);
            ex.ValidationErrors.Errors[0].ShouldBe(ApplicationProperties.Resources.Product_Validator_NotExists
                .Replace("{PropertyName}", nameof(UpdateProductCommand.ProductUpdateDto.Id)));

            var products = await _mockRepo.Object.GetAllAsync();
            products.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Update_Product_With_Invalid_Category_Test()
        {
            _productDto.Category = (Category)3;
            var ex = await Should.ThrowAsync<ValidationException>
                (async () => await _handler.Handle
                    (new UpdateProductCommand() { ProductUpdateDto = _productDto }, CancellationToken.None));
            ex.ValidationErrors.Errors.Count.ShouldBe(1);
            ex.ValidationErrors.Errors[0].ShouldBe(ApplicationProperties.Resources.Product_Validator_InvalidEnum
                .Replace("{PropertyName}", nameof(UpdateProductCommand.ProductUpdateDto.Category)));

            var products = await _mockRepo.Object.GetAllAsync();
            products.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Update_Product_With_Invalid_Description_Test()
        {
            _productDto.Description = null;
            var ex = await Should.ThrowAsync<ValidationException>
                (async () => await _handler.Handle
                    (new UpdateProductCommand() { ProductUpdateDto = _productDto }, CancellationToken.None));
            ex.ValidationErrors.Errors.Count.ShouldBe(1);
            // MyString = ApplicationProperties.Resources.Product_Validator_Required.Replace("{PropertyName}", nameof(_productDto.Description));
            ex.ValidationErrors.Errors[0].ShouldBe(ApplicationProperties.Resources.Product_Validator_Required
                .Replace("{PropertyName}", nameof(UpdateProductCommand.ProductUpdateDto.Description)));

            // invalid description - too long
            _productDto.Description = TestProperties.Resources.Test_TooLongString_1001;
            ex = await Should.ThrowAsync<ValidationException>
                (async () => await _handler.Handle
                    (new UpdateProductCommand() { ProductUpdateDto = _productDto }, CancellationToken.None));
            ex.ValidationErrors.Errors.Count.ShouldBe(1);
            MyString = ApplicationProperties.Resources.Product_Validator_TooLong.Replace("{PropertyName}", nameof(_productDto.Description));
            MyString = MyString.Replace("{MaxLength}", "1000");

            var products = await _mockRepo.Object.GetAllAsync();
            products.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Update_Product_With_Invalid_Name_Test()
        {
            // invalid name - null
            _productDto.Name = null;
            var response = await _handler.Handle
                (new UpdateProductCommand() { ProductUpdateDto = _productDto }, CancellationToken.None);
            response.ShouldBeOfType<BaseCommandResponse>();
            MyString = ApplicationProperties.Resources.Product_Validator_Required.Replace("{PropertyName}", nameof(_productDto.Name));

            // invalid name - too long
            _productDto.Name = TestProperties.Resources.Test_TooLongString_101;
            response = await _handler.Handle
                (new UpdateProductCommand() { ProductUpdateDto = _productDto }, CancellationToken.None);
            response.ShouldBeOfType<BaseCommandResponse>();
            MyString = ApplicationProperties.Resources.Product_Validator_TooLong.Replace("{PropertyName}", nameof(_productDto.Name));
            MyString = MyString.Replace("{MaxLength}", "100");

            var products = await _mockRepo.Object.GetAllAsync();
            products.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Update_Product_With_Invalid_Price_Tes()
        {
            _productDto.Price = 10.002m;
            var response = await _handler.Handle
                (new UpdateProductCommand() { ProductUpdateDto = _productDto }, CancellationToken.None);
            response.ShouldBeOfType<BaseCommandResponse>();

            var products = await _mockRepo.Object.GetAllAsync();
            products.Count.ShouldBe(3);
        }
    }
}

﻿using AutoMapper;
using Kolisetka.Application.Contracts.Persistence;
using Kolisetka.Application.DTOs.DtoProduct;
using Kolisetka.Application.Features.Products.Handlers.Commands;
using Kolisetka.Application.Features.Products.Requests.Commands;
using Kolisetka.Application.Profiles;
using Kolisetka.Application.Responses;
using Kolisetka.Application.UnitTests.Mocks;
using Kolisetka.Domain;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

using ApplicationProperties = Kolisetka.Application.Properties;
using TestProperties = Kolisetka.Application.UnitTests.Properties;

namespace Kolisetka.Application.UnitTests.Products.Commands
{
    public class CreateProductCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly CreateProductCommandHandler _handler;
        private readonly ProductCreateDto _productDto;

        private string MyString;

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
        public async Task Valid_Product_Added()
        {
            var response = await _handler.Handle
                (new CreateProductCommand() { ProductCreateDto = _productDto }, CancellationToken.None);
            response.ShouldBeOfType<BaseCommandResponse>();
            response.Success.ShouldBeTrue();

            var products = await _mockRepo.Object.GetAllAsync();
            products.Count.ShouldBe(4);
        }

        [Fact]
        public async Task Invalid_Product_Added()
        {
            // invalid price - invalid precision
            _productDto.Price = 10.002m;
            var response = await _handler.Handle
                (new CreateProductCommand() { ProductCreateDto = _productDto }, CancellationToken.None);
            response.ShouldBeOfType<BaseCommandResponse>();
            response.Errors.ShouldNotBeNull();
            response.Errors.Count.ShouldBe(1);
            response.Errors[0].ShouldBe(ApplicationProperties.Resources.Product_Validator_InvalidPrecision.Replace("{PropertyName}", nameof(_productDto.Price)));

            // invalid category - invalid enum
            _productDto.Price = 10.00m;
            _productDto.Category = (Category)3;
            response = await _handler.Handle
                (new CreateProductCommand() { ProductCreateDto = _productDto }, CancellationToken.None);
            response.ShouldBeOfType<BaseCommandResponse>();
            response.Errors.ShouldNotBeNull();
            response.Errors.Count.ShouldBe(1);
            response.Errors[0].ShouldBe(ApplicationProperties.Resources.Product_Validator_InvalidEnum.Replace("{PropertyName}", nameof(_productDto.Category)));

            // invalid name - null
            _productDto.Category = Category.Food;
            _productDto.Name = null;
            response = await _handler.Handle
                (new CreateProductCommand() { ProductCreateDto = _productDto }, CancellationToken.None);
            response.ShouldBeOfType<BaseCommandResponse>();
            response.Errors.ShouldNotBeNull();
            response.Errors.Count.ShouldBe(1);
            response.Errors[0].ShouldBe(ApplicationProperties.Resources.Product_Validator_Required.Replace("{PropertyName}", nameof(_productDto.Name)));

            // invalid name - too long
            _productDto.Name = TestProperties.Resources.Test_TooLongString_101;
            response = await _handler.Handle
                (new CreateProductCommand() { ProductCreateDto = _productDto }, CancellationToken.None);
            response.ShouldBeOfType<BaseCommandResponse>();
            response.Errors.ShouldNotBeNull();
            response.Errors.Count.ShouldBe(1);
            MyString = ApplicationProperties.Resources.Product_Validator_TooLong.Replace("{PropertyName}", nameof(_productDto.Name));
            MyString = MyString.Replace("{MaxLength}", "100");
            response.Errors[0].ShouldBe(MyString);

            // invalid description - null
            _productDto.Name = "Zapiekanka";
            _productDto.Description = null;
            response = await _handler.Handle
                (new CreateProductCommand() { ProductCreateDto = _productDto }, CancellationToken.None);
            response.ShouldBeOfType<BaseCommandResponse>();
            response.Errors.ShouldNotBeNull();
            response.Errors.Count.ShouldBe(1);
            response.Errors[0].ShouldBe(ApplicationProperties.Resources.Product_Validator_Required.Replace("{PropertyName}", nameof(_productDto.Description)));

            // invalid description - too long
            _productDto.Description = TestProperties.Resources.Test_TooLongString_1001;
            response = await _handler.Handle
                (new CreateProductCommand() { ProductCreateDto = _productDto }, CancellationToken.None);
            response.ShouldBeOfType<BaseCommandResponse>();
            response.Errors.ShouldNotBeNull();
            response.Errors.Count.ShouldBe(1);
            MyString = ApplicationProperties.Resources.Product_Validator_TooLong.Replace("{PropertyName}", nameof(_productDto.Description));
            MyString = MyString.Replace("{MaxLength}", "1000");
            response.Errors[0].ShouldBe(MyString);

            var products = await _mockRepo.Object.GetAllAsync();
            products.Count.ShouldBe(3);
        }
    }
}

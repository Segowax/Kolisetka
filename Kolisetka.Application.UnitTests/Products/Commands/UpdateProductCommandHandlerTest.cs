using AutoMapper;
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
        public async Task Valid_Product_Update()
        {
            var response = await _handler.Handle
                (new UpdateProductCommand() { ProductUpdateDto = _productDto }, CancellationToken.None);
            response.ShouldBeOfType<BaseCommandResponse>();
            response.Success.ShouldBeTrue();

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
        public async Task Invalid_Product_Update()
        {
            // invalid id
            _productDto.Id = 5;
            var response = await _handler.Handle
                (new UpdateProductCommand() { ProductUpdateDto = _productDto }, CancellationToken.None);
            response.ShouldBeOfType<BaseCommandResponse>();
            response.Success.ShouldBeFalse();
            response.Errors.ShouldNotBeNull();
            response.Errors.Count.ShouldBe(1);
            response.Errors[0].ShouldBe(ApplicationProperties.Resources.Product_Validator_NotExists.Replace("{PropertyName}", nameof(_productDto.Id)));

            // invalid category
            _productDto.Id = 2;
            _productDto.Category = (Category)3;
            response = await _handler.Handle
                (new UpdateProductCommand() { ProductUpdateDto = _productDto }, CancellationToken.None);
            response.ShouldBeOfType<BaseCommandResponse>();
            response.Success.ShouldBeFalse();
            response.Errors.ShouldNotBeNull();
            response.Errors.Count.ShouldBe(1);
            response.Errors[0].ShouldBe(ApplicationProperties.Resources.Product_Validator_InvalidEnum.Replace("{PropertyName}", nameof(_productDto.Category)));

            // invalid description
            _productDto.Category = Category.Food;
            _productDto.Description = TestProperties.Resources.Test_TooLongString_1001;
            response = await _handler.Handle
                (new UpdateProductCommand() { ProductUpdateDto = _productDto }, CancellationToken.None);
            response.ShouldBeOfType<BaseCommandResponse>();
            response.Success.ShouldBeFalse();
            MyString = ApplicationProperties.Resources.Product_Validator_TooLong.Replace("{PropertyName}", nameof(_productDto.Description));
            MyString = MyString.Replace("{MaxLength}", "1000");
            response.Errors[0].ShouldBe(MyString);

            // invalid name
            _productDto.Description = "Najsmaczniejsza golonka na całym Kozanownie!";
            _productDto.Name = TestProperties.Resources.Test_TooLongString_101;
            response = await _handler.Handle
                (new UpdateProductCommand() { ProductUpdateDto = _productDto }, CancellationToken.None);
            response.ShouldBeOfType<BaseCommandResponse>();
            response.Success.ShouldBeFalse();
            response.Errors.ShouldNotBeNull();
            response.Errors.Count.ShouldBe(1);
            MyString = ApplicationProperties.Resources.Product_Validator_TooLong.Replace("{PropertyName}", nameof(_productDto.Name));
            MyString = MyString.Replace("{MaxLength}", "100");
            response.Errors[0].ShouldBe(MyString);

            // invalid price
            _productDto.Name = "Golonka";
            _productDto.Price = 10.002m;
            response = await _handler.Handle
                (new UpdateProductCommand() { ProductUpdateDto = _productDto }, CancellationToken.None);
            response.ShouldBeOfType<BaseCommandResponse>();
            response.Success.ShouldBeFalse();
            response.Errors.ShouldNotBeNull();
            response.Errors.Count.ShouldBe(1);
            response.Errors[0].ShouldBe(ApplicationProperties.Resources.Product_Validator_InvalidPrecision.Replace("{PropertyName}", nameof(_productDto.Price)));
        }
    }
}

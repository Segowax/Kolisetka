using AutoMapper;
using Kolisetka.Application.Contracts.Persistence;
using Kolisetka.Application.DTOs.DtoProduct;
using Kolisetka.Application.Features.Products.Handlers.Commands;
using Kolisetka.Application.Features.Products.Requests.Commands;
using Kolisetka.Application.Profiles;
using Kolisetka.Application.Responses;
using Kolisetka.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

using ApplicationProperties = Kolisetka.Application.Properties;

namespace Kolisetka.Application.UnitTests.Products.Commands
{
    public class DeleteProductCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly DeleteProductCommandHandler _handler;
        private readonly ProductDeleteDto _productDto;

        public DeleteProductCommandHandlerTest()
        {
            _mockRepo = MockProductRepository.GetProductsRepository();

            var mapperConfig = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _handler = new DeleteProductCommandHandler(_mockRepo.Object, _mapper);

            _productDto = new ProductDeleteDto
            {
                Id = 2
            };
        }

        [Fact]
        public async Task Valid_Product_Deleted()
        {
            var response = await _handler.Handle
                (new DeleteProductCommand() { ProductDeleteDto = _productDto }, CancellationToken.None);
            response.Success.ShouldBeTrue();

            var products = await _mockRepo.Object.GetAllAsync();
            products.Count.ShouldBe(2);

            foreach (var product in products)
            {
                (product.Id == 1 || product.Id == 3).ShouldBeTrue();
            }

            // trying to delete the same id as previous one
            response = await _handler.Handle
                (new DeleteProductCommand() { ProductDeleteDto = _productDto }, CancellationToken.None);
            response.ShouldBeOfType<BaseCommandResponse>();
            response.Success.ShouldBeFalse();
            response.Errors.ShouldNotBeNull();
            response.Errors.Count.ShouldBe(1);
            response.Errors[0].ShouldBe(ApplicationProperties.Resources.Product_Validator_NotExists.Replace("{PropertyName}", nameof(_productDto.Id)));
        }

        [Fact]
        public async Task Invalid_Product_Deleted()
        {
            // invalid Id
            _productDto.Id = 10;
            var response = await _handler.Handle
                (new DeleteProductCommand() { ProductDeleteDto = _productDto }, CancellationToken.None);
            response.ShouldBeOfType<BaseCommandResponse>();
            response.Success.ShouldBeFalse();
            response.Errors.ShouldNotBeNull();
            response.Errors.Count.ShouldBe(1);
            response.Errors[0].ShouldBe(ApplicationProperties.Resources.Product_Validator_NotExists.Replace("{PropertyName}", nameof(_productDto.Id)));

            var products = await _mockRepo.Object.GetAllAsync();
            products.Count.ShouldBe(3);
        }
    }
}

using AutoMapper;
using Kolisetka.Application.Contracts.Persistence;
using Kolisetka.Application.DTOs.DtoProduct;
using Kolisetka.Application.Features.Products.Handlers.Commands;
using Kolisetka.Application.Features.Products.Requests.Commands;
using Kolisetka.Application.Profiles;
using Kolisetka.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

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
        public async Task Delete_Valid_Product_Test()
        {
            await _handler.Handle
                (new DeleteProductCommand() { ProductDeleteDto = _productDto }, CancellationToken.None);

            var products = await _mockRepo.Object.GetAllAsync();
            products.Count.ShouldBe(2);

            foreach (var product in products)
            {
                (product.Id == 1 || product.Id == 3).ShouldBeTrue();
            }

            // trying to delete the same id as previous one
            await _handler.Handle
                (new DeleteProductCommand() { ProductDeleteDto = _productDto }, CancellationToken.None);
        }

        [Fact]
        public async Task Delete_Invalid_Product_Test()
        {
            // invalid Id
            _productDto.Id = 10;
            await _handler.Handle
                (new DeleteProductCommand() { ProductDeleteDto = _productDto }, CancellationToken.None);

            var products = await _mockRepo.Object.GetAllAsync();
            products.Count.ShouldBe(3);
        }
    }
}

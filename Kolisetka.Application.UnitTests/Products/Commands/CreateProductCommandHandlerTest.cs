using AutoMapper;
using Kolisetka.Application.Contracts.Persistence;
using Kolisetka.Application.DTOs.DtoProduct;
using Kolisetka.Application.Features.Products.Handlers.Commands;
using Kolisetka.Application.Features.Products.Requests.Commands;
using Kolisetka.Application.Profiles;
using Kolisetka.Application.UnitTests.Mocks;
using MediatR;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Kolisetka.Application.UnitTests.Products.Commands
{
    public class CreateProductCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly ProductCreateDto _productDto;

        public CreateProductCommandHandlerTest()
        {
            _mockRepo = MockProductRepository.GetProductsRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _productDto = new ProductCreateDto
            {
                Category = 0,
                Description = "Na mały głód.",
                Name = "Zapiekanka",
                Price = 10.00m
            };
        }

        [Fact]
        public async Task Valid_Product_Added()
        {
            var handler = new CreateProductCommandHandler(_mockRepo.Object, _mapper);

            await handler.Handle
                (new CreateProductCommand() { CreateProductDto = _productDto }, CancellationToken.None);

            var products = await _mockRepo.Object.GetAllAsync();
            products.Count.ShouldBe(4);
        }
    }
}

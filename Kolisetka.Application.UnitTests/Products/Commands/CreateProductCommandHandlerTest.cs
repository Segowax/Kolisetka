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
using System.Threading;
using System.Threading.Tasks;
using Xunit;

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
        public async Task Valid_Product_Added()
        {
            await _handler.Handle
                (new CreateProductCommand() { CreateProductDto = _productDto }, CancellationToken.None);

            var products = await _mockRepo.Object.GetAllAsync();
            products.Count.ShouldBe(4);
        }

        [Fact]
        public async Task Invalid_Product_Added()
        {
            // invalid price
            _productDto.Price = 10.002m;
            ValidationException ex = await Should.ThrowAsync<ValidationException>
                (async () => await _handler.Handle
                    (new CreateProductCommand() { CreateProductDto = _productDto }, CancellationToken.None));

            // invalid category
            _productDto.Price = 10.00m;
            _productDto.Category = (Category)3;
            ex = await Should.ThrowAsync<ValidationException>
                (async () => await _handler.Handle
                    (new CreateProductCommand() { CreateProductDto = _productDto }, CancellationToken.None));

            // invalid name
            _productDto.Category = Category.Food;
            _productDto.Name = null;
            ex = await Should.ThrowAsync<ValidationException>
                (async () => await _handler.Handle
                    (new CreateProductCommand() { CreateProductDto = _productDto }, CancellationToken.None));

            // invalid description
            _productDto.Name = "Zapiekanka";
            _productDto.Description = null;
            ex = await Should.ThrowAsync<ValidationException>
                (async () => await _handler.Handle
                    (new CreateProductCommand() { CreateProductDto = _productDto }, CancellationToken.None));

            var products = await _mockRepo.Object.GetAllAsync();
            products.Count.ShouldBe(3);
        }
    }
}

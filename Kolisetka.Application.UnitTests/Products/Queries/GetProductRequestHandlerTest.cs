using AutoMapper;
using Kolisetka.Application.Contracts.Persistence;
using Kolisetka.Application.DTOs.DtoProduct;
using Kolisetka.Application.Features.Products.Handlers.Queries;
using Kolisetka.Application.Features.Products.Requests.Queries;
using Kolisetka.Application.Profiles;
using Kolisetka.Application.UnitTests.Mocks;
using Kolisetka.Domain;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Kolisetka.Application.UnitTests.Products.Queries
{
    public class GetProductRequestHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _mockRepo;

        public GetProductRequestHandlerTest()
        {
            _mockRepo = MockProductRepository.GetProductsRepository();

            var mapperConfig = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Get_Product_With_Success_Test()
        {
            var handler = new GetProductRequestHandler(_mockRepo.Object, _mapper);
            var result = await handler.Handle(new GetProductRequest() { Id = 1 }, CancellationToken.None);

            result.ShouldBeOfType<ProductGetDto>();
            result.Category.ShouldBe(Category.AlkoholicDrink);
            result.DateCreated.ShouldBe(new DateTime(2022, 6, 1));
            result.DateUpdated.ShouldBe(new DateTime(2022, 6, 1));
            result.Description.ShouldBe("Piwo 500 ml, czeskie z nalewaka.");
            result.Name.ShouldBe("Holba");
            result.Price.ShouldBe(6.00m);
        }
    }
}

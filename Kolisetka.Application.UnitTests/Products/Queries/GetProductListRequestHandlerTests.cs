using AutoMapper;
using Kolisetka.Application.Contracts.Persistence;
using Kolisetka.Application.DTOs.DtoProduct;
using Kolisetka.Application.Features.Products.Handlers.Queries;
using Kolisetka.Application.Features.Products.Requests.Queries;
using Kolisetka.Application.Profiles;
using Kolisetka.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Kolisetka.Application.UnitTests.Products.Queries
{
    public class GetProductListRequestHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _mockRepo;

        public GetProductListRequestHandlerTests()
        {
            _mockRepo = MockProductRepository.GetProductsRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Get_Product_List_With_Success_Test()
        {
            var handler = new GetProductsListRequestHandler(_mockRepo.Object, _mapper);
            var result = await handler.Handle(new GetProductsListRequest(), CancellationToken.None);

            result.ShouldBeOfType<List<ProductGetDto>>();
            result.Count.ShouldBe(3);
        }
    }
}

using AutoMapper;
using Kolisetka.Application.Contracts.Identity;
using Kolisetka.Application.DTOs.DtoUser;
using Kolisetka.Application.Features.User.Handlers.Queries;
using Kolisetka.Application.Features.User.Requests.Queries;
using Kolisetka.Application.Profiles;
using Kolisetka.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Kolisetka.Application.UnitTests.Users.Queries
{
    public class GetUsersListRequestHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserRepository> _mockRepo;

        public GetUsersListRequestHandlerTest()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _mockRepo = MockUserRepository.GetUserRepository();
        }

        [Fact]
        public async Task Get_Users_List_WithSuccess_Test()
        {
            var handler = new GetUsersListRequestHandler(_mockRepo.Object, _mapper);
            var result = await handler.Handle(new GetUsersListRequest { }, CancellationToken.None);

            result.Count.ShouldBe(2);
            result.ShouldBeOfType(typeof(List<UserGetDto>));
        }
    }
}

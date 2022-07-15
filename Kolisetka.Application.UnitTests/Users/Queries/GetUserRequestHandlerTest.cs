using Kolisetka.Application.Contracts.Identity;
using Kolisetka.Application.Features.User.Handlers.Queries;
using Kolisetka.Application.Features.User.Requests.Queries;
using Kolisetka.Application.Responses;
using Kolisetka.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

using ApplicationProperties = Kolisetka.Application.Properties;
using TestProperties = Kolisetka.Application.UnitTests.Properties;

namespace Kolisetka.Application.UnitTests.Users.Queries
{
    public class GetUserRequestHandlerTest
    {
        private readonly Mock<IAuthRepository> _mockRepo;

        public GetUserRequestHandlerTest()
        {
            _mockRepo = MockAuthRepository.GetAuthRepository();
        }

        [Fact]
        public async Task Get_User_With_Success_Test()
        {
            var handler = new GetUserRequestHandler(_mockRepo.Object);
            var result = await handler.Handle
                (new GetUserRequest
                {
                    Email = TestProperties.Resources.Test_ValidUser_Email,
                    Password = TestProperties.Resources.Test_ValidUser_Password
                }, CancellationToken.None);

            result.ShouldBeOfType<AuthResponse>();
            result.Success.ShouldBeTrue();
            result.Email.ShouldBe(TestProperties.Resources.Test_ValidUser_Email);
            result.Message.ShouldBeNull();
            result.Id.ShouldBe(TestProperties.Resources.Test_ValidUser_Id);
            result.Token.ShouldNotBeNull();
            result.UserName.ShouldBe(TestProperties.Resources.Test_ValidUser_UserName);
        }

        [Fact]
        public async Task Get_User_With_Failure_Test()
        {
            var handler = new GetUserRequestHandler(_mockRepo.Object);
            var result = await handler.Handle
                (new GetUserRequest
                {
                    Email = TestProperties.Resources.Test_InvalidUser_Email,
                    Password = TestProperties.Resources.Test_InvalidUser_Password
                }, CancellationToken.None);

            result.ShouldBeOfType<AuthResponse>();
            result.Success.ShouldBeFalse();
            result.Message.ShouldBe(ApplicationProperties.Resources.User_Validator_NotExistsOrInvalidPassword);
            result.Token.ShouldBeNull();
            result.Email.ShouldBeNull();
            result.Id.ShouldBeNull();
            result.UserName.ShouldBeNull();
        }
    }
}

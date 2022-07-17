using Kolisetka.Application.Contracts.Identity;
using Kolisetka.Application.UnitTests.Mocks;
using Moq;

namespace Kolisetka.Application.UnitTests.Users.Commands
{
    public class CreateUserCommandHandlerTest
    {
        private readonly Mock<IAuthRepository> _mockRepo;

        public CreateUserCommandHandlerTest()
        {
            _mockRepo = MockAuthRepository.GetAuthRepository();
        }
    }
}

using AutoMapper;
using Kolisetka.Application.Contracts.Identity;
using Kolisetka.Application.DTOs.DtoUser;
using Kolisetka.Application.Features.User.Handlers.Commands;
using Kolisetka.Application.Features.User.Requests.Commands;
using Kolisetka.Application.Profiles;
using Kolisetka.Application.Responses;
using Kolisetka.Application.UnitTests.Mocks;
using Microsoft.AspNetCore.Identity;
using Moq;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

using ApplicationProperties = Kolisetka.Application.Properties;
using TestProperties = Kolisetka.Application.UnitTests.Properties;

namespace Kolisetka.Application.UnitTests.Users.Commands
{
    public class CreateUserCommandHandlerTest
    {
        private readonly Mock<IAuthRepository> _mockRepo;
        private readonly IMapper _mapper;
        private readonly UserCreateDto _validUserDto;
        private readonly UserCreateDto _invalidUserDto;

        public CreateUserCommandHandlerTest()
        {
            _mockRepo = MockAuthRepository.GetAuthRepository();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            CreateUsers(out _validUserDto, out _invalidUserDto);
        }

        private static void CreateUsers(out UserCreateDto validUser, out UserCreateDto invalidUser)
        {
            validUser = new UserCreateDto()
            {
                Email = TestProperties.Resources.Test_ValidUser_NewEmail,
                EmailConfirmed = true,
                FirstName = TestProperties.Resources.Test_ValidUser_NewFirstName,
                LastName = TestProperties.Resources.Test_ValidUser_NewLastName,
                Password = TestProperties.Resources.Test_ValidUser_NewPassword,
                UserName = TestProperties.Resources.Test_ValidUser_NewUserName
            };

            invalidUser = new UserCreateDto()
            {
                Email = TestProperties.Resources.Test_InvalidUser_NewEmail,
                EmailConfirmed = true,
                FirstName = TestProperties.Resources.Test_ValidUser_NewFirstName,
                LastName = TestProperties.Resources.Test_ValidUser_NewLastName,
                Password = TestProperties.Resources.Test_ValidUser_NewPassword,
                UserName = TestProperties.Resources.Test_ValidUser_NewUserName
            };
        }

        [Fact]
        public async Task Register_User_With_Success_Test()
        {
            var handler = new CreateUserCommandHandler(_mockRepo.Object, _mapper);
            var result = await handler.Handle
                (new CreateUserCommand
                {
                    UserCreateDto = _validUserDto
                }, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeTrue();

            var users = MockAuthRepository.GetUsers();
            var hasher = MockAuthRepository.GetHasher();
            users.Count.ShouldBe(3);

            var newUser = users.LastOrDefault();
            newUser.EmailConfirmed.ShouldBeTrue();
            newUser.Email.ShouldBe(TestProperties.Resources.Test_ValidUser_NewEmail);
            newUser.FirstName.ShouldBe(TestProperties.Resources.Test_ValidUser_NewFirstName);
            newUser.LastName.ShouldBe(TestProperties.Resources.Test_ValidUser_NewLastName);
            newUser.UserName.ShouldBe(TestProperties.Resources.Test_ValidUser_NewUserName);
            hasher.VerifyHashedPassword(newUser, newUser.PasswordHash, _validUserDto.Password)
                .ShouldBe(PasswordVerificationResult.Success);
        }

        [Fact]
        public async Task Register_User_With_Failure_Test()
        {
            var handler = new CreateUserCommandHandler(_mockRepo.Object, _mapper);
            var result = await handler.Handle
                (new CreateUserCommand
                {
                    UserCreateDto = _invalidUserDto
                }, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeFalse();

            var users = MockAuthRepository.GetUsers();
            users.Count.ShouldBe(2);

            // invalid email
            result.Errors[0]
                .ShouldBe(ApplicationProperties.Resources.User_Validator_Email
                    .Replace("{PropertyName}", nameof(_invalidUserDto.Email)));
        }
    }
}

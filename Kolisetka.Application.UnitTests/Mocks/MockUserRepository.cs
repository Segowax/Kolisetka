using Kolisetka.Application.Contracts.Identity;
using Kolisetka.Application.Features.User.Requests.Queries;
using Kolisetka.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Collections.Generic;

namespace Kolisetka.Application.UnitTests.Mocks
{
    internal class MockUserRepository
    {
        private static readonly List<User> _users;
        private static readonly PasswordHasher<User> _hasher;

        static MockUserRepository()
        {
            _hasher = new PasswordHasher<User>();
            _users = MockAuthRepository.GetUsersList(_hasher);
        }

        internal static Mock<IUserRepository> GetUserRepository()
        {
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(r => r.GetAllUsersAsync()).ReturnsAsync(_users);

            return mockRepo;
        }
    }
}

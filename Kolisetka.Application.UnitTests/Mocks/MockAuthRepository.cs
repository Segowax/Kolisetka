using Kolisetka.Application.Contracts.Identity;
using Kolisetka.Application.Features.User.Requests.Queries;
using Kolisetka.Application.Properties;
using Kolisetka.Application.Responses;
using Kolisetka.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kolisetka.Application.UnitTests.Mocks
{
    internal class MockAuthRepository
    {
        private static readonly List<User> _users;
        private static readonly PasswordHasher<User> _hasher;

        static MockAuthRepository()
        {
            _hasher = new PasswordHasher<User>();
            _users = GetUsersList(_hasher);
        }

        internal static Mock<IAuthRepository> GetAuthRepository()
        {
            var mockRepo = new Mock<IAuthRepository>();
            mockRepo.Setup(r => r.Login(It.IsAny<GetUserRequest>())).ReturnsAsync((GetUserRequest queryUser) =>
            {
                var response = new AuthResponse();
                var responseUser = new User();
                foreach (var user in _users)
                {
                    if (queryUser.Email.Equals(user.Email))
                    {
                        responseUser = user;
                        break;
                    }
                }
                var result = _hasher.VerifyHashedPassword(responseUser, responseUser.PasswordHash, queryUser.Password);
                if (result == PasswordVerificationResult.Failed)
                {
                    response.Success = false;
                    response.Message = Resources.User_Validator_NotExistsOrInvalidPassword;

                    return response;
                }

                response.Email = responseUser.Email;
                response.Id = responseUser.Id;
                response.Token = Guid.NewGuid().ToString().ToUpper();
                response.UserName = responseUser.UserName;
                response.Success = true;

                return response;
            });
            mockRepo.Setup(r => r.Register(It.IsAny<User>(), It.IsAny<string>())).Returns((User newUser, string password) =>
            {
                newUser.Id = Guid.NewGuid().ToString().ToUpper();
                newUser.ConcurrencyStamp = Guid.NewGuid().ToString().ToUpper();
                newUser.SecurityStamp = Guid.NewGuid().ToString().ToUpper();
                newUser.PasswordHash = _hasher.HashPassword(newUser, password);
                _users.Add(newUser);

                return Task.CompletedTask;
            });

            mockRepo.Setup(r => r.IsEmailExist(It.IsAny<string>())).ReturnsAsync((string email) =>
            {
                foreach (var user in _users)
                {
                    if (user.Email.Equals(email))
                        return true;
                }

                return false;
            });

            mockRepo.Setup(r => r.IsUserNameExist(It.IsAny<string>())).ReturnsAsync((string userName) =>
            {
                foreach (var user in _users)
                {
                    if (user.UserName.Equals(userName))
                        return true;
                }

                return false;
            });

            return mockRepo;
        }

        private static List<User> GetUsersList(PasswordHasher<User> _hasher)
        {
            var users = new List<User>
            {
                new User
                {
                    Id = "0be5b79b-d566-4fb1-b9f7-9aa1115d889b",
                    Email = "user@localhost.com",
                    NormalizedEmail = "USER@LOCALHOST.COM",
                    FirstName = "System",
                    LastName = "User",
                    UserName = "user@localhost.com",
                    NormalizedUserName = "USER@LOCALHOST.COM",
                    EmailConfirmed = true
                },
                new User
                {
                    Id = "cfd8e76a-aa81-41b3-8623-a6882006a126",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "ADMIN@LOCALHOST.COM",
                    FirstName = "System",
                    LastName = "Admin",
                    UserName = "admin@localhost.com",
                    NormalizedUserName = "ADMIN@LOCALHOST.COM",
                    EmailConfirmed = true
                }
            };
            foreach (var user in users)
                user.PasswordHash = _hasher.HashPassword(user, "kotyToBumBum!2");

            return users;
        }

        internal static List<User> GetUsers()
        {
            return _users;
        }

        internal static PasswordHasher<User> GetHasher()
        {
            return _hasher;
        }
    }
}

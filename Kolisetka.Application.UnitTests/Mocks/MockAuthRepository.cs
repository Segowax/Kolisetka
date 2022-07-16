using Kolisetka.Application.Contracts.Identity;
using Kolisetka.Application.Features.User.Requests.Queries;
using Kolisetka.Application.Properties;
using Kolisetka.Application.Responses;
using Kolisetka.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;

namespace Kolisetka.Application.UnitTests.Mocks
{
    internal class MockAuthRepository
    {
        internal static Mock<IAuthRepository> GetAuthRepository()
        {
            var hasher = new PasswordHasher<User>();
            var users = GetUsersList(hasher);

            var mockRepo = new Mock<IAuthRepository>();
            mockRepo.Setup(r => r.Login(It.IsAny<GetUserRequest>())).ReturnsAsync((GetUserRequest queryUser) =>
            {
                var response = new AuthResponse();
                var responseUser = new User();
                foreach (var user in users)
                {
                    if(queryUser.Email.Equals(user.Email))
                    {
                        responseUser = user;
                        break;
                    }
                }
                var result = hasher.VerifyHashedPassword(responseUser, responseUser.PasswordHash, queryUser.Password);
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
            mockRepo.Setup(r => r.IsEmailExist(It.IsAny<string>())).ReturnsAsync((string email) =>
            {
                foreach (var user in users)
                {
                    if (user.Email.Equals(email))
                        return true;
                }

                return false;
            });

            return mockRepo;
        }

        private static List<User> GetUsersList(PasswordHasher<User> hasher)
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
                user.PasswordHash = hasher.HashPassword(user, "kotyToBumBum");

            return users;
        }
    }
}

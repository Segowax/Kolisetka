﻿using Kolisetka.Application.Contracts.Identity;
using Kolisetka.Application.Features.User.Requests.Queries;
using Kolisetka.Application.Properties;
using Kolisetka.Application.Responses;
using Kolisetka.Application.Validators;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kolisetka.Application.Features.User.Handlers.Queries
{
    public class GetUserRequestHandler : IRequestHandler<GetUserRequest, AuthResponse>
    {
        private readonly IAuthRepository _authRepository;

        public GetUserRequestHandler(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<AuthResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            var response = new AuthResponse();
            var validator = new UserGetValidator(_authRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = Resources.User_Validator_NotExistsOrInvalidPassword;
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).Distinct().ToList();

                return response;
            }
            else
            {
                response.Success = true;
            }

            return await _authRepository.Login(request);
        }
    }
}

using AutoMapper;
using Kolisetka.Application.Contracts.Identity;
using Kolisetka.Application.Features.User.Requests.Commands;
using Kolisetka.Application.Properties;
using Kolisetka.Application.Responses;
using Kolisetka.Application.Validators;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MyUser = Kolisetka.Domain.Models;

namespace Kolisetka.Application.Features.User.Handlers.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, BaseCommandResponse>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UserCreateValidator(_authRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = Resources.User_Creation_Failure;
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).Distinct().ToList();

                return response;
            }
            else
            {
                response.Success = true;
                response.Message = Resources.User_Creation_Success;
            }
            var user = _mapper.Map<MyUser.User>(request.User);

            await _authRepository.Register(user, request.Password);

            return response;
        }
    }
}

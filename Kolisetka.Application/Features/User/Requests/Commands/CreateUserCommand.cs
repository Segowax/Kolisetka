using Kolisetka.Application.DTOs.DtoUser;
using Kolisetka.Application.Responses;
using MediatR;

namespace Kolisetka.Application.Features.User.Requests.Commands
{
    public class CreateUserCommand : IRequest<BaseCommandResponse>
    {
        public UserCreateDto UserCreateDto { get; set; }
    }
}

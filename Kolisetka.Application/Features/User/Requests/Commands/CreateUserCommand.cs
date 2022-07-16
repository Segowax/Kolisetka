using Kolisetka.Application.DTOs.DtoUser;
using Kolisetka.Application.Responses;
using MediatR;

namespace Kolisetka.Application.Features.User.Requests.Commands
{
    public class CreateUserCommand : IRequest<BaseCommandResponse>
    {
        public UserCreateDto User { get; set; }
        public string Password { get; set; }
    }
}

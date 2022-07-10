using MediatR;

namespace Kolisetka.Application.Features.User.Requests.Commands
{
    public class CreateUserCommand : IRequest<Unit>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

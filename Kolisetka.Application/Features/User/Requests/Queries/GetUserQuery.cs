using Kolisetka.Application.Responses;
using MediatR;

namespace Kolisetka.Application.Features.User.Requests.Queries
{
    public class GetUserQuery : IRequest<AuthResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

using Kolisetka.Application.Features.User.Requests.Commands;
using Kolisetka.Application.Features.User.Requests.Queries;
using Kolisetka.Application.Responses;
using System.Threading.Tasks;

namespace Kolisetka.Application.Contracts.Persistence
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(GetAuthUserQuery query);
        Task Register(CreateUserCommand command);
    }
}

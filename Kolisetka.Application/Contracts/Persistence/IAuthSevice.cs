using Kolisetka.Application.Features.User.Requests.Commands;
using Kolisetka.Application.Features.User.Requests.Queries;
using Kolisetka.Application.Responses;
using System.Threading.Tasks;

namespace Kolisetka.Application.Contracts.Persistence
{
    public interface IAuthSevice
    {
        Task<AuthResponse> Login(GetAuthUserQuery query);
        Task Register(CreateUserCommand command);
    }
}

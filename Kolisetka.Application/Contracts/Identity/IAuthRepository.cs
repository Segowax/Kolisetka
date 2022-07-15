using Kolisetka.Application.Features.User.Requests.Commands;
using Kolisetka.Application.Features.User.Requests.Queries;
using Kolisetka.Application.Responses;
using System.Threading.Tasks;

namespace Kolisetka.Application.Contracts.Identity
{
    public interface IAuthRepository
    {
        Task<AuthResponse> Login(GetUserRequest query);
        Task Register(CreateUserCommand command);
        Task<bool> IsExist(string email);
    }
}

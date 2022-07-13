using Kolisetka.Application.Features.User.Requests.Commands;
using Kolisetka.Application.Features.User.Requests.Queries;
using Kolisetka.Application.Responses;
using System.Threading.Tasks;

namespace Kolisetka.Application.Contracts.Persistence
{
    public interface IAuthRepository
    {
        Task<AuthResponse> Login(GetUserQuery query);
        Task Register(CreateUserCommand command);
        Task<bool> IsExist(string email);
    }
}

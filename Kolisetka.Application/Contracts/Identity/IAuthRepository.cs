using Kolisetka.Application.Features.User.Requests.Queries;
using Kolisetka.Application.Responses;
using Kolisetka.Domain.Models;
using System.Threading.Tasks;

namespace Kolisetka.Application.Contracts.Identity
{
    public interface IAuthRepository
    {
        Task<AuthResponse> Login(GetUserRequest query);
        Task Register(User command, string password);
        Task<bool> IsEmailExist(string email);
        Task<bool> IsUserNameExist(string userName);
    }
}

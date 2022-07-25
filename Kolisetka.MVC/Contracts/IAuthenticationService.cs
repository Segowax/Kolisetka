using Kolisetka.MVC.Models.User;

namespace Kolisetka.MVC.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> Authenticate(LoginVM userLogin);
        Task<bool> Register(RegisterVM userRegister);
        Task Logout();
    }
}

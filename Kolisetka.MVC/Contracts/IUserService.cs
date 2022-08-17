using Kolisetka.MVC.Models.User;

namespace Kolisetka.MVC.Contracts
{
    public interface IUserService
    {
        Task<List<UserGetVM>> GetUsers();
    }
}

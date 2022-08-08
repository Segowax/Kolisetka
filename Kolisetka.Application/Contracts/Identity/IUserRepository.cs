using Kolisetka.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kolisetka.Application.Contracts.Identity
{
    public  interface IUserRepository
    {
        Task<List<User>> GetUsers();
    }
}

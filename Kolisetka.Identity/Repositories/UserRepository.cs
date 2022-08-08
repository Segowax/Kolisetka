using Kolisetka.Application.Contracts.Identity;
using Kolisetka.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kolisetka.Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _userManager.Users.ToListAsync();
        }
    }
}

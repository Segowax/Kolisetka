using AutoMapper;
using Kolisetka.MVC.Contracts;
using Kolisetka.MVC.Models.User;
using Kolisetka.MVC.Services.Base;

namespace Kolisetka.MVC.Services
{
    public class UserService : BaseHttpService, IUserService
    {
        private new readonly ILocalStorageService _localStorageService;
        private new readonly IClient _client;
        private readonly IMapper _mapper;

        public UserService(ILocalStorageService localStorageService, IMapper mapper, IClient client)
            : base(client, localStorageService)
        {
            _localStorageService = localStorageService;
            _mapper = mapper;
            _client = client;
        }

        public async Task<List<UserGetVM>> GetUsers()
        {
            AddBearerToken();
            var users = await _client.UserAsync();

            return _mapper.Map<List<UserGetVM>>(users);
        }
    }
}

using AutoMapper;
using Kolisetka.MVC.Contracts;
using Kolisetka.MVC.Models.User;
using Kolisetka.MVC.Services.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Kolisetka.MVC.Services
{
    public class AuthenticationService : BaseHttpService, Contracts.IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public AuthenticationService
            (IClient client, ILocalStorageService localStorage
            , IHttpContextAccessor httpContextAccessor, IMapper mapper)
            : base(client, localStorage)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public async Task<bool> Authenticate(LoginVM userLogin)
        {
            try
            {
                var authenticationRequest = _mapper.Map<GetUserRequest>(userLogin);
                var authenticationResponse = await _client.LoginAsync(authenticationRequest);
                if (authenticationResponse.Token != string.Empty)
                {
                    // Get Claims from token and build auth user object
                    var tokenContent = _tokenHandler.ReadJwtToken(authenticationResponse.Token);
                    var claims = ParseClaims(tokenContent);
                    var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
                    var login = _httpContextAccessor.HttpContext?.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
                    _localStorageService.SetStorageValue("token", authenticationResponse.Token);

                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private IList<Claim> ParseClaims(JwtSecurityToken tokenContent)
        {
            var claims = tokenContent.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));

            return claims;
        }

        public async Task Logout()
        {
            _localStorageService.ClearStorage(new List<string> { "token" });
            if (_httpContextAccessor.HttpContext is null) return;
            await _httpContextAccessor.HttpContext.SignOutAsync
                (CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<bool> Register(RegisterVM userRegister)
        {
            AddBearerToken();
            var registerRequest = _mapper.Map<UserCreateDto>(userRegister);
            var response = await _client.RegisterAsync(registerRequest);
            if (response.Success == true)
                return true;

            return false;
        }
    }
}

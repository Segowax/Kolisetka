using Kolisetka.Application.Contracts.Identity;
using Kolisetka.Application.Features.User.Requests.Queries;
using Kolisetka.Application.Properties;
using Kolisetka.Application.Responses;
using Kolisetka.Application.Settings;
using Kolisetka.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Kolisetka.Identity.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IOptions<JwtSettings> _jwtSettings;

        public AuthRepository
            (UserManager<User> userManager, SignInManager<User> signInManager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings;
        }

        public async Task<AuthResponse> Login(GetUserRequest query)
        {
            var response = new AuthResponse();
            var user = await _userManager.FindByEmailAsync(query.Email);
            var result = await _signInManager.PasswordSignInAsync(query.Email, query.Password, false, false);
            if (!result.Succeeded)
            {
                response.Success = false;
                response.Message = Resources.User_Validator_NotExistsOrInvalidPassword;

                return response;
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);
            response.Email = user.Email;
            response.Id = user.Id;
            response.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.UserName = user.UserName;
            response.Success = true;

            return response;
        }

        private async Task<JwtSecurityToken> GenerateToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                new Claim("id", user.Id),
            }
            .Union(userClaims)
            .Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Value.Issuer,
                audience: _jwtSettings.Value.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.Value.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public async Task Register(User command, string password)
        {
            var result = await _userManager.CreateAsync(command, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(command, "User");                
            }
        }

        public async Task<bool> IsEmailExist(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return user != null;
        }

        public async Task<bool> IsUserNameExist(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            return user != null;
        }
    }
}

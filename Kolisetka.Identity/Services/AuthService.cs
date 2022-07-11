using Kolisetka.Application.Contracts.Persistence;
using Kolisetka.Application.Features.User.Requests.Commands;
using Kolisetka.Application.Features.User.Requests.Queries;
using Kolisetka.Application.Responses;
using Kolisetka.Application.Settings;
using Kolisetka.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Kolisetka.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService
            (UserManager<User> userManager, SignInManager<User> signInManager, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings;
        }

        public async Task<AuthResponse> Login(GetAuthUserQuery query)
        {
            var user = await _userManager.FindByEmailAsync(query.Email);
            if (user is null)
                throw new Exception($"Iinvalid password or with {query.Email} email does not exist!");
            var result = await _signInManager.PasswordSignInAsync(user.UserName, user.PasswordHash, false, false);
            if (!result.Succeeded)
                throw new Exception($"Iinvalid password or with {query.Email} email does not exist!");

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            return new AuthResponse
            {
                Email = user.Email,
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = user.UserName
            };
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
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public Task Register(CreateUserCommand command)
        {
            throw new NotImplementedException();
        }
    }
}

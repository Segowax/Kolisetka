using Kolisetka.Application.Contracts.Persistence;
using Kolisetka.Application.Features.User.Requests.Queries;
using Kolisetka.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Kolisetka.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authSevice)
        {
            _authService = authSevice;
        }

        [HttpGet("login")]
        public async Task<ActionResult<AuthResponse>> Login(GetAuthUserQuery query)
        {
            var response = await _authService.Login(query);

            return Ok(response);
        }
    }
}

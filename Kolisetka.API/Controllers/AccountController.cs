using Kolisetka.Application.DTOs.DtoUser;
using Kolisetka.Application.Features.User.Requests.Commands;
using Kolisetka.Application.Features.User.Requests.Queries;
using Kolisetka.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kolisetka.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(GetUserRequest query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpPost("register")]
#if !DEBUGNOAUTH
        [Authorize(Roles = "Admin")]
#endif
        public async Task<ActionResult<BaseCommandResponse>> Register(UserCreateDto command)
        {
            var response = await _mediator.Send(new CreateUserCommand() { UserCreateDto = command });

            return Ok(response);
        }
    }
}

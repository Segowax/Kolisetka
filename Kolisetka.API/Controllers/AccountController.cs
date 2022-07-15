using Kolisetka.Application.Features.User.Requests.Queries;
using Kolisetka.Application.Responses;
using MediatR;
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
    }
}

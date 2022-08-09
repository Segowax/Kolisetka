using Kolisetka.Application.DTOs.DtoUser;
using Kolisetka.Application.Features.User.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kolisetka.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(401)]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<UserController>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<UserGetDto>), 200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<IReadOnlyList<UserGetDto>>> Get()
        {
            var users = await _mediator.Send(new GetUsersListRequest());

            return Ok(users);
        }
    }
}

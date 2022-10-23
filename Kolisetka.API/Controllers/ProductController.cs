using Kolisetka.Application.DTOs.DtoProduct;
using Kolisetka.Application.Exceptions;
using Kolisetka.Application.Features.Products.Requests.Commands;
using Kolisetka.Application.Features.Products.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kolisetka.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
#if !DEBUGNOAUTH
    [Authorize]
#endif
    [ProducesResponseType(401)]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<ProductController>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<ProductGetDto>), 200)]
        public async Task<ActionResult<IReadOnlyList<ProductGetDto>>> Get()
        {
            var products = await _mediator.Send(new GetProductsListRequest());

            return Ok(products);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductGetDto), 200)]
        public async Task<ActionResult<ProductGetDto>> Get(int id)
        {
            var product = await _mediator.Send(new GetProductRequest { Id = id });

            return Ok(product);
        }

        // POST api/<ProductController>
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(List<string>), 422)]
        public async Task<IActionResult> Post([FromBody] ProductCreateDto product)
        {
            try
            {
                await _mediator.Send(new CreateProductCommand { ProductCreateDto = product });
            }
            catch (ValidationException ex)
            {
                return UnprocessableEntity(ex.Errors);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // PUT api/<ProductController>
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(List<string>), 422)]
        public async Task<IActionResult> Put([FromBody] ProductUpdateDto product)
        {
            try
            {
                var response = await _mediator.Send(new UpdateProductCommand { ProductUpdateDto = product });
            }
            catch(ValidationException ex)
            {
                return UnprocessableEntity(ex.Errors);
            }
            catch(Exception)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // DELETE api/<ProductController>
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(List<string>), 422)]
        public async Task<ActionResult> Delete(ProductDeleteDto product)
        {
            try
            {
                await _mediator.Send(new DeleteProductCommand { ProductDeleteDto = product });
            }
            catch (ValidationException ex)
            {
                return UnprocessableEntity(ex.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return NoContent();
        }

    }
}

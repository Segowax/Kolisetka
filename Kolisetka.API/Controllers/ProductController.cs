using Kolisetka.Application.DTOs.DtoProduct;
using Kolisetka.Application.Features.Products.Requests.Commands;
using Kolisetka.Application.Features.Products.Requests.Queries;
using Kolisetka.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kolisetka.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductGetDto>>> Get()
        {
            var products = await _mediator.Send(new GetProductsListRequest());

            return Ok(products);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductGetDto>> Get(int id)
        {
            var product = await _mediator.Send(new GetProductRequest { Id = id });

            return Ok(product);
        }

        // POST api/<ProductController>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] ProductCreateDto product)
        {
            var response = await _mediator.Send(new CreateProductCommand { ProductCreateDto = product });

            return Ok(response);
        }

        // PUT api/<ProductController>
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<BaseCommandResponse>> Put([FromBody] ProductUpdateDto product)
        {
            var response = await _mediator.Send(new UpdateProductCommand { ProductUpdateDto = product });

            return Ok(response);
        }

        // DELETE api/<ProductController>
        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<BaseCommandResponse>> Delete(ProductDeleteDto product)
        {
            var response = await _mediator.Send(new DeleteProductCommand { ProductDeleteDto = product });

            return Ok(response);
        }

    }
}

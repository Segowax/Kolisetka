using Kolisetka.Application.DTOs.DtoProduct;
using Kolisetka.Application.Features.Products.Requests.Commands;
using Kolisetka.Application.Features.Products.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kolisetka.Api.Controllers
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
        public async Task<ActionResult> Post([FromBody] ProductCreateDto product)
        {
            await _mediator.Send(new CreateProductCommand { CreateProductDto = product });

            return NoContent();
        }

        // PUT api/<ProductController>
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] ProductUpdateDto product)
        {
            await _mediator.Send(new UpdateProductCommand { ProductUpdateDto = product });

            return NoContent();
        }

        // DELETE api/<ProductController>
        [HttpDelete]
        public async Task<ActionResult> Delete(ProductDeleteDto product)
        {
            await _mediator.Send(new DeleteProductCommand { ProductDeleteDto = product });

            return NoContent();
        }
    }
}

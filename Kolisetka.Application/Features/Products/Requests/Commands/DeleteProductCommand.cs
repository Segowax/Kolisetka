using Kolisetka.Application.DTOs.DtoProduct;
using MediatR;

namespace Kolisetka.Application.Features.Products.Requests.Commands
{
    public class DeleteProductCommand : IRequest
    {
        public ProductDeleteDto ProductDto { get; set; }
    }
}

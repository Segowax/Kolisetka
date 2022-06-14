using Kolisetka.Application.DTOs.DtoProduct;
using MediatR;

namespace Kolisetka.Application.Features.Products.Requests.Commands
{
    public class UpdateProductCommand : IRequest
    {
        public ProductUpdateDto ProductUpdateDto { get; set; }
    }
}

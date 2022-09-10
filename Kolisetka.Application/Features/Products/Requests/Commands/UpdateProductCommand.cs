using Kolisetka.Application.DTOs.DtoProduct;
using MediatR;

namespace Kolisetka.Application.Features.Products.Requests.Commands
{
    public class UpdateProductCommand : IRequest<Unit>
    {
        public ProductUpdateDto ProductUpdateDto { get; set; }
    }
}

using Kolisetka.Application.DTOs.DtoProduct;
using MediatR;

namespace Kolisetka.Application.Features.Products.Requests.Commands
{
    public class CreateProductCommand : IRequest<Unit>
    {
        public ProductCreateDto ProductCreateDto { get; set; }
    }
}

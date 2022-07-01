using Kolisetka.Application.DTOs.DtoProduct;
using Kolisetka.Application.Responses;
using MediatR;

namespace Kolisetka.Application.Features.Products.Requests.Commands
{
    public class CreateProductCommand : IRequest<BaseCommandResponse>
    {
        public ProductCreateDto ProductCreateDto { get; set; }
    }
}

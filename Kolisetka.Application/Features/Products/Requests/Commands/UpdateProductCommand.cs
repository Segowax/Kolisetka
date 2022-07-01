using Kolisetka.Application.DTOs.DtoProduct;
using Kolisetka.Application.Responses;
using MediatR;

namespace Kolisetka.Application.Features.Products.Requests.Commands
{
    public class UpdateProductCommand : IRequest<BaseCommandResponse>
    {
        public ProductUpdateDto ProductUpdateDto { get; set; }
    }
}

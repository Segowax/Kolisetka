using Kolisetka.Application.DTOs.DtoProduct;
using Kolisetka.Application.Responses;
using MediatR;

namespace Kolisetka.Application.Features.Products.Requests.Commands
{
    public class DeleteProductCommand : IRequest<BaseCommandResponse>
    {
        public ProductDeleteDto ProductDeleteDto { get; set; }
    }
}

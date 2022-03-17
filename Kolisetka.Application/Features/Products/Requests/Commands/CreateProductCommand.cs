using Kolisetka.Application.DTOs;
using MediatR;

namespace Kolisetka.Application.Features.Products.Requests.Commands
{
    public class CreateProductCommand : IRequest
    {
        public ProductCreateDto CreateProductDto { get; set; }
    }
}

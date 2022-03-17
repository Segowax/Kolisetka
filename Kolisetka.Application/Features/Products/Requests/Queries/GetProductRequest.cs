using Kolisetka.Application.DTOs;
using MediatR;

namespace Kolisetka.Application.Features.Products.Requests.Queries
{
    public class GetProductRequest : IRequest<ProductDto>
    {
        public int Id { get; set; }
    }
}

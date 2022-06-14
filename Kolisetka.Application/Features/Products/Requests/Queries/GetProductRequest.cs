using Kolisetka.Application.DTOs.DtoProduct;
using MediatR;

namespace Kolisetka.Application.Features.Products.Requests.Queries
{
    public class GetProductRequest : IRequest<ProductGetDto>
    {
        public int Id { get; set; }
    }
}

using Kolisetka.Application.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Kolisetka.Application.Features.Products.Requests.Queries
{
    public class GetProductsListRequest : IRequest<IReadOnlyList<ProductDto>>
    {
    }
}

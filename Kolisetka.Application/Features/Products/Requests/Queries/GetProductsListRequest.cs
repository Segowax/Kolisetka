using Kolisetka.Application.DTOs.DtoProduct;
using MediatR;
using System.Collections.Generic;

namespace Kolisetka.Application.Features.Products.Requests.Queries
{
    public class GetProductsListRequest : IRequest<IReadOnlyList<ProductGetDto>> { }
}

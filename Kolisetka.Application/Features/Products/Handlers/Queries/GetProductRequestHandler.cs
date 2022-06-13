using AutoMapper;
using Kolisetka.Application.DTOs;
using Kolisetka.Application.DTOs.DtoProduct;
using Kolisetka.Application.Features.Products.Requests.Queries;
using Kolisetka.Application.Contracts.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kolisetka.Application.Features.Products.Handlers.Queries
{
    public class GetProductRequestHandler : IRequestHandler<GetProductRequest, ProductUpdateDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductRequestHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductUpdateDto> Handle(GetProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetAsync(request.Id);

            return _mapper.Map<ProductUpdateDto>(product);
        }
    }
}

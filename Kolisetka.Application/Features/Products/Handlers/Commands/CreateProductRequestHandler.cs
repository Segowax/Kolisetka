using AutoMapper;
using Kolisetka.Application.DTOs;
using Kolisetka.Application.Features.Products.Requests.Commands;
using Kolisetka.Application.Persistence.Contracts;
using Kolisetka.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kolisetka.Application.Features.Products.Handlers.Commands
{
    public class CreateProductRequestHandler : IRequestHandler<CreateProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateProductRequestHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<ProductCreateDto, Product>(request.CreateProductDto);
            await _productRepository.AddAsync(product);

            return Unit.Value;
        }
    }
}

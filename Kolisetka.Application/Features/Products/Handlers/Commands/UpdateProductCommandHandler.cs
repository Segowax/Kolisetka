using AutoMapper;
using Kolisetka.Application.Features.Products.Requests.Commands;
using Kolisetka.Application.Persistence.Contracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kolisetka.Application.Features.Products.Handlers.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetAsync(request.ProductDto.Id);
            _mapper.Map(request.ProductDto, product);
            await _productRepository.UpdateAsync(product);

            return Unit.Value;
        }
    }
}

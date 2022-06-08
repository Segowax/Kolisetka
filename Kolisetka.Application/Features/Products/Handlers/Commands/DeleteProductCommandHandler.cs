using AutoMapper;
using Kolisetka.Application.DTOs.Validators;
using Kolisetka.Application.Exceptions;
using Kolisetka.Application.Features.Products.Requests.Commands;
using Kolisetka.Application.Persistence.Contracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kolisetka.Application.Features.Products.Handlers.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public DeleteProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new ProductDeleteValidator(_productRepository);
            var validationResult = await validator.ValidateAsync(request.ProductDto);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var product = await _productRepository.GetAsync(request.ProductDto.Id);
            _mapper.Map(request.ProductDto, product);

            await _productRepository.DeleteAsync(product);

            return Unit.Value;
        }
    }
}

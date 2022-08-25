using AutoMapper;
using Kolisetka.Application.Validators;
using Kolisetka.Application.Features.Products.Requests.Commands;
using Kolisetka.Application.Contracts.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Kolisetka.Application.Exceptions;

namespace Kolisetka.Application.Features.Products.Handlers.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
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
            var validationResult = await validator.ValidateAsync(request.ProductDeleteDto);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var product = await _productRepository.GetAsync(request.ProductDeleteDto.Id);
            _mapper.Map(request.ProductDeleteDto, product);

            await _productRepository.DeleteAsync(product);

            return Unit.Value;
        }
    }
}

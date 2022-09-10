using AutoMapper;
using Kolisetka.Application.Validators;
using Kolisetka.Application.Features.Products.Requests.Commands;
using Kolisetka.Application.Contracts.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Kolisetka.Application.Properties;
using Kolisetka.Application.Exceptions;

namespace Kolisetka.Application.Features.Products.Handlers.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
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
            var validator = new ProductUpdateValidator(_productRepository);
            var validationResult = await validator.ValidateAsync(request.ProductUpdateDto);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var product = await _productRepository.GetAsync(request.ProductUpdateDto.Id);
            _mapper.Map(request.ProductUpdateDto, product); // Map new product (request.Product) -> old product (product)
            await _productRepository.UpdateAsync(product);

            return Unit.Value;
        }
    }
}

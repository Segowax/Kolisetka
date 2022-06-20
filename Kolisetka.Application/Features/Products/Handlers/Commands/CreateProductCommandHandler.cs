using AutoMapper;
using Kolisetka.Application.DTOs.DtoProduct;
using Kolisetka.Application.DTOs.Validators;
using Kolisetka.Application.Exceptions;
using Kolisetka.Application.Features.Products.Requests.Commands;
using Kolisetka.Application.Contracts.Persistence;
using Kolisetka.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kolisetka.Application.Features.Products.Handlers.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new ProductCreateValidator();
            var validationResult = await validator.ValidateAsync(request.ProductCreateDto);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var product = _mapper.Map<ProductCreateDto, Product>(request.ProductCreateDto);
            await _productRepository.AddAsync(product);

            return Unit.Value;
        }
    }
}

using AutoMapper;
using Kolisetka.Application.DTOs;
using Kolisetka.Application.DTOs.Validators;
using Kolisetka.Application.Features.Products.Requests.Commands;
using Kolisetka.Application.Persistence.Contracts;
using Kolisetka.Domain;
using MediatR;
using System;
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
            var validator = new ProductCreateValidator();
            var validationResult = await validator.ValidateAsync(request.CreateProductDto);

            if (!validationResult.IsValid)
                throw new Exception();

            var product = _mapper.Map<ProductCreateDto, Product>(request.CreateProductDto);
            await _productRepository.AddAsync(product);

            return Unit.Value;
        }
    }
}

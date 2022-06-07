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
            var validator = new ProductUpdateValidator(_productRepository);
            var validatorResult = await validator.ValidateAsync(request.ProductDto);

            if (!validatorResult.IsValid)
                throw new Exception();

            var product = _mapper.Map<ProductDto, Product>(request.ProductDto);
            await _productRepository.UpdateAsync(product);

            return Unit.Value;
        }
    }
}

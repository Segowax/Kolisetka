﻿using AutoMapper;
using Kolisetka.Application.DTOs;
using Kolisetka.Application.DTOs.Validators;
using Kolisetka.Application.Exceptions;
using Kolisetka.Application.Features.Products.Requests.Commands;
using Kolisetka.Application.Contracts.Persistence;
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
                throw new ValidationException(validatorResult);

            var product = await _productRepository.GetAsync(request.ProductDto.Id);
            _mapper.Map(request.ProductDto, product); // Map new product (request.Product) -> old product (product)

            await _productRepository.UpdateAsync(product);

            return Unit.Value;
        }
    }
}

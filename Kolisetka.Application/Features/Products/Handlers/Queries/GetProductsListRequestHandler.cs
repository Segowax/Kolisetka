﻿using AutoMapper;
using Kolisetka.Application.DTOs.DtoProduct;
using Kolisetka.Application.Features.Products.Requests.Queries;
using Kolisetka.Application.Contracts.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kolisetka.Application.Features.Products.Handlers.Queries
{
    public class GetProductsListRequestHandler : IRequestHandler<GetProductsListRequest, IReadOnlyList<ProductGetDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsListRequestHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ProductGetDto>> Handle
            (GetProductsListRequest request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();

            return _mapper.Map<IReadOnlyList<ProductGetDto>>(products);
        }
    }
}

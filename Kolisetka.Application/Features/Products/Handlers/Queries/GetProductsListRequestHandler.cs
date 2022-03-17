﻿using AutoMapper;
using Kolisetka.Application.DTOs;
using Kolisetka.Application.Features.Products.Requests.Queries;
using Kolisetka.Application.Persistence.Contracts;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kolisetka.Application.Features.Products.Handlers.Queries
{
    public class GetProductsListRequestHandler : IRequestHandler<GetProductsListRequest, IReadOnlyList<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsListRequestHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ProductDto>> Handle
            (GetProductsListRequest request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();

            return _mapper.Map<IReadOnlyList<ProductDto>>(products);
        }
    }
}

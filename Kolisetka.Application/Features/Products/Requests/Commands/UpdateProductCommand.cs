﻿using Kolisetka.Application.DTOs;
using MediatR;

namespace Kolisetka.Application.Features.Products.Requests.Commands
{
    public class UpdateProductCommand : IRequest
    {
        public ProductDto ProductDto { get; set; }
    }
}
﻿using Kolisetka.Domain;

namespace Kolisetka.Application.DTOs.DtoProduct
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
    }
}

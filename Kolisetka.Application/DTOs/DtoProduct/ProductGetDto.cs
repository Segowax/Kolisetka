using Kolisetka.Application.DTOs.Common;
using Kolisetka.Domain;
using System;

namespace Kolisetka.Application.DTOs.DtoProduct
{
    public class ProductGetDto : BaseDto
    {
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
    }
}

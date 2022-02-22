using Kolisetka.Application.DTOs.Common;
using Kolisetka.Domain;

namespace Kolisetka.Application.DTOs
{
    public class ProductDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
    }
}

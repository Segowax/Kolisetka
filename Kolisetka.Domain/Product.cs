using Kolisetka.Domain.Common;

namespace Kolisetka.Domain
{
    public class Product : Base
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}

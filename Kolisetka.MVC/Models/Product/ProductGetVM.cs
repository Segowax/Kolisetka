using Kolisetka.MVC.Models.Base;
using Kolisetka.MVC.Services.Base;

namespace Kolisetka.MVC.Models.Product
{
    public class ProductGetVM : ProductVM
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
    }
}

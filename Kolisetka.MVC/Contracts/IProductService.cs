using Kolisetka.MVC.Models.Product;
using Kolisetka.MVC.Services.Base;

namespace Kolisetka.MVC.Contracts
{
    public interface IProductService
    {
        Task<List<ProductGetVM>> GetProducts();
        Task<ProductGetVM> GetProductDetails(int id);
        Task<Response> CreateProduct(ProductCreateVM product);
        Task<Response> UpdateProduct(ProductUpdateVM product);
        Task<Response> DeleteProduct(ProductDeleteVM product);
    }
}

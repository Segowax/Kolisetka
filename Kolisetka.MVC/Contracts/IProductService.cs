using Kolisetka.MVC.Models.Product;
using Kolisetka.MVC.Services.Base;

namespace Kolisetka.MVC.Contracts
{
    public interface IProductService
    {
        Task<List<ProductGetVM>> GetProducts();
        Task<ProductGetVM> GetProductDetails(int id);
        Task<Response<int>> CreateProduct(ProductCreateVM product);
        Task<Response<int>> UpdateProduct(ProductUpdateVM product);
        Task<Response<int>> DeleteProduct(ProductDeleteVM product);
    }
}

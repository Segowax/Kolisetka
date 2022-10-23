using AutoMapper;
using Kolisetka.MVC.Contracts;
using Kolisetka.MVC.Models.Product;
using Kolisetka.MVC.Services.Base;

namespace Kolisetka.MVC.Services
{
    public class ProductService : BaseHttpService, IProductService
    {
        private new readonly ILocalStorageService _localStorageService;
        private new readonly IClient _client;
        private readonly IMapper _mapper;

        public ProductService(ILocalStorageService localStorageService, IMapper mapper, IClient client)
            : base(client, localStorageService)
        {
            _localStorageService = localStorageService;
            _mapper = mapper;
            _client = client;
        }

        public async Task<Response> CreateProduct(ProductCreateVM product)
        {
            try
            {
                AddBearerToken();
                var productToAdd = _mapper.Map<ProductCreateDto>(product);
                await _client.ProductPOSTAsync(productToAdd);

                return new Response() 
                { 
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }

        public async Task<Response> DeleteProduct(ProductDeleteVM product)
        {
            try
            {
                AddBearerToken();
                var productToDelete = _mapper.Map<ProductDeleteDto>(product);
                await _client.ProductDELETEAsync(productToDelete);

                return new Response()
                {
                    Success = true
                };
            }
            catch (ApiException ex)
            {

                return ConvertApiExceptions(ex);
            }
        }

        public async Task<ProductGetVM> GetProductDetails(int id)
        {
            AddBearerToken();
            var product = await _client.ProductGETAsync(id);

            return _mapper.Map<ProductGetVM>(product);
        }

        public async Task<List<ProductGetVM>> GetProducts()
        {
            AddBearerToken();
            var products = await _client.ProductAllAsync();

            return _mapper.Map<List<ProductGetVM>>(products);
        }

        public async Task<Response> UpdateProduct(ProductUpdateVM product)
        {
            try
            {
                AddBearerToken();
                var productToUpdate = _mapper.Map<ProductUpdateDto>(product);
                await _client.ProductPUTAsync(productToUpdate);

                return new Response()
                {
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }
    }
}

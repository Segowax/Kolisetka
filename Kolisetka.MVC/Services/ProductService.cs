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

        public async Task<Response<int>> CreateProduct(ProductCreateVM product)
        {
            try
            {
                var response = new Response<int>();
                var productToAdd = _mapper.Map<ProductCreateDto>(product);
                var apiResponse = await _client.ProductPOSTAsync(productToAdd);
                if (apiResponse.Success)
                    response.Success = true;
                else
                    foreach (var error in apiResponse.Errors)
                    {
                        response.ValidationError += error + Environment.NewLine;
                    }

                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }

        public async Task<Response<int>> DeleteProduct(ProductDeleteVM product)
        {
            try
            {
                var response = new Response<int>();
                var productToDelete = _mapper.Map<ProductDeleteDto>(product);
                var apiResponse = await _client.ProductDELETEAsync(productToDelete);
                if (apiResponse.Success)
                    response.Success = true;
                else
                    foreach (var error in apiResponse.Errors)
                    {
                        response.ValidationError += error + Environment.NewLine;
                    }

                return response;
            }
            catch (ApiException ex)
            {

                return ConvertApiExceptions<int>(ex);
            }
        }

        public async Task<ProductGetVM> GetProductDetails(int id)
        {
            var product = await _client.ProductGETAsync(id);

            return _mapper.Map<ProductGetVM>(product);
        }

        public async Task<List<ProductGetVM>> GetProducts()
        {
            var products = await _client.ProductAllAsync();

            return _mapper.Map<List<ProductGetVM>>(products);
        }

        public async Task<Response<int>> UpdateProduct(ProductUpdateVM product)
        {
            try
            {
                var response = new Response<int>();
                var productToUpdate = _mapper.Map<ProductUpdateDto>(product);
                var apiResponse = await _client.ProductPUTAsync(productToUpdate);
                if (apiResponse.Success)
                    response.Success = true;
                else
                    foreach (var error in apiResponse.Errors)
                    {
                        response.ValidationError += error + Environment.NewLine;
                    }

                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }
    }
}

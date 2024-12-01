using Mango.Frontend.MVC.Models.Dtos;
using Mango.Frontend.MVC.Services.Interfaces;
using System.Runtime.CompilerServices;

namespace Mango.Frontend.MVC.Services
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _config;
        private readonly IBaseService _baseService;
        private readonly string _productApiUrl = string.Empty;
        private readonly string CONTROLLER_ROUTE = "api/product";


        public ProductService(IConfiguration config, IBaseService baseService)
        {
            _config = config;
            _baseService = baseService;
            string? productApiUrl = _config.GetValue<string>("Services:ProductAPI");

            if (string.IsNullOrWhiteSpace(productApiUrl) == false)
            {
                _productApiUrl = $"{productApiUrl}/{CONTROLLER_ROUTE}";
            }
        }

        public Task<ResponseDto?> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> CreateProductAsync(ProductDto ProductDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> UpdateProductAsync(ProductDto ProductCode)
        {
            throw new NotImplementedException();
        }
    }
}

using Mango.Frontend.MVC.Enums;
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

        public async Task<ResponseDto?> GetAllProductsAsync()
        {
            RequestDto requestDto = new RequestDto
            {
                Url = _productApiUrl,
            };

            ResponseDto? responseDto = await _baseService.SendAsync(requestDto);

            return responseDto;
        }

        public async Task<ResponseDto?> GetProductByIdAsync(int id)
        {
            RequestDto requestDto = new()
            {
                Url = $"{_productApiUrl}/{id}",
            };

            ResponseDto? responseDto = await _baseService.SendAsync(requestDto);
            return responseDto;
        }

        public async Task<ResponseDto?> CreateProductAsync(ProductDto productDto)
        {
            RequestDto requestDto = new()
            {
                HttpVerb = ApiTypeEnum.POST,
                Data = productDto,
                Url = _productApiUrl
            };

            ResponseDto? responseDto = await _baseService.SendAsync(requestDto);
            return responseDto;
        }

        public async Task<ResponseDto?> UpdateProductAsync(ProductDto productDto)
        {
            RequestDto requestDto = new()
            {
                HttpVerb = ApiTypeEnum.PUT,
                Data = productDto,
                Url = _productApiUrl
            };

            ResponseDto? responseDto = await _baseService.SendAsync(requestDto);
            return responseDto;
        }

        public async Task<ResponseDto?> DeleteProductAsync(int id)
        {
            RequestDto requestDto = new()
            {
                HttpVerb= ApiTypeEnum.DELETE,
                Url = $"{_productApiUrl}/{id}"
            };

            ResponseDto? responseDto = await _baseService.SendAsync(requestDto);
            return responseDto;
        }
    }
}

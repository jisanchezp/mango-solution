using Mango.Frontend.MVC.Models.Dtos;

namespace Mango.Frontend.MVC.Services.Interfaces
{
    public interface IProductService
    {
        Task<ResponseDto?> GetAllProductsAsync();
        Task<ResponseDto?> GetProductByIdAsync(int id);
        Task<ResponseDto?> CreateProductAsync(ProductDto ProductDto);
        Task<ResponseDto?> UpdateProductAsync(ProductDto ProductCode);
        Task<ResponseDto?> DeleteProductAsync(int id);
    }
}

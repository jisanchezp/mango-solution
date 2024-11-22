using Mango.Frontend.MVC.Models.Dtos;

namespace Mango.Frontend.MVC.Services.Interfaces
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto);
    }
}

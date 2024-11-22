using Mango.Frontend.MVC.Models.Dtos;

namespace Mango.Frontend.MVC.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        Task<string> RegisterAsync(RegistrationRequestDto registrationRequestDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}

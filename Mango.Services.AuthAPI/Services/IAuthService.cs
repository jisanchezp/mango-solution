using Mango.Services.AuthAPI.Models.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;

namespace Mango.Services.AuthAPI.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        Task<bool> AssignRoleAsync(string email, string roleName);
    }
}

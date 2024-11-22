using Mango.Frontend.MVC.Models.Dtos;
using Mango.Frontend.MVC.Services.Interfaces;

namespace Mango.Frontend.MVC.Services
{
    public class AuthService : IAuthService
    {
        public Task<bool> AssignRole(string email, string roleName)
        {
        }

        public Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task<string> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}

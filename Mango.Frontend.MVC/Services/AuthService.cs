using Mango.Frontend.MVC.Enums;
using Mango.Frontend.MVC.Models.Dtos;
using Mango.Frontend.MVC.Services.Interfaces;

namespace Mango.Frontend.MVC.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IBaseService _baseService;
        private readonly string _authApiUrl = string.Empty;
        private readonly string CONTROLLER_ROUTE = "api/auth";

        public AuthService(IConfiguration config, IBaseService baseService)
        {
            _config = config;
            _baseService = baseService;
            string? authApiUrl = _config.GetValue<string>("Services:AuthAPI");

            if (string.IsNullOrWhiteSpace(_authApiUrl) == false)
            {
                _authApiUrl = $"{authApiUrl}/{CONTROLLER_ROUTE}";
            }
        }

        public async Task<ResponseDto?> AssignRole(RegistrationRequestDto registrationRequestDto)
        {
            RequestDto requestDto = new()
            {
                HttpVerb = ApiTypeEnum.POST,
                Url = $"{_authApiUrl}/assign-role",
                Data = registrationRequestDto,
            };

            ResponseDto? response = await _baseService.SendAsync(requestDto);
            return response;
        }

        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            RequestDto requestDto = new()
            {
                HttpVerb = ApiTypeEnum.POST,
                Url = $"{_authApiUrl}/login",
                Data = loginRequestDto
            };

            ResponseDto? response = await _baseService.SendAsync(requestDto);
            return response;
        }

        public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            RequestDto requestDto = new()
            {
                HttpVerb = ApiTypeEnum.POST,
                Url = $"{_authApiUrl}/register",
                Data = registrationRequestDto
            };

            ResponseDto? response = await _baseService.SendAsync(requestDto);
            return response;            
        }
    }
}

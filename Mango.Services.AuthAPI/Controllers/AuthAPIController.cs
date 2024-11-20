using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {

        private readonly IAuthService _authService;
        protected ResponseDto _response;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            var output = await _authService.Register(registrationRequestDto);

            if (string.IsNullOrWhiteSpace(output) == false)
            {
                _response.IsSucess = false;
                _response.Message = output;

                return BadRequest(_response);
            }

            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var output = await _authService.Login(loginRequestDto);

            if (output.User is null)
            {
                _response.IsSucess = false;
                _response.Message = "Username or passwword is incorrect";
                BadRequest(_response);
            }

            _response.Result = output;
            return Ok(_response);
        }
    } 
}

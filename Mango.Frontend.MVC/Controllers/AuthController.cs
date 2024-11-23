using Mango.Frontend.MVC.Constants;
using Mango.Frontend.MVC.Helper;
using Mango.Frontend.MVC.Models.Dtos;
using Mango.Frontend.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Mango.Frontend.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;

        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            ResponseDto? responseDto = await _authService.LoginAsync(loginRequestDto);

            if (responseDto is not null && responseDto.IsSuccess)
            {
                LoginResponseDto loginResponseDto = JsonHelper.DeserializeCaseInsensitive<LoginResponseDto>(Convert.ToString(responseDto.Result)!)!;

                await SignInUser(loginResponseDto);
                _tokenProvider.SetToken(loginResponseDto.Token);

                TempData["success"] = "Login successful! :)";
                return RedirectToAction(nameof(Index), "Home");
            }

            TempData["error"] = responseDto.Message;
            return View(loginRequestDto);            
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.RoleList = RoleConstants.rolesSelectList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto registrationRequestDto)
        {
            ResponseDto? registerResponse = await _authService.RegisterAsync(registrationRequestDto);
            ResponseDto? assignRoleResponse = null;
            var errorMessage = registerResponse.Message;
            if (registerResponse is not null && registerResponse.IsSuccess)
            {
                if (string.IsNullOrWhiteSpace(registrationRequestDto.Role))
                {
                    registrationRequestDto.Role = RoleConstants.USER_ROLE;
                }

                assignRoleResponse = await _authService.AssignRoleAsync(registrationRequestDto);

                if (assignRoleResponse is not null && assignRoleResponse.IsSuccess)
                {
                    TempData["success"] = "Registration Successful";
                    return RedirectToAction(nameof(Login));
                }

                errorMessage = assignRoleResponse.Message;
            }

            TempData["error"] = errorMessage;
            ViewBag.RoleList = RoleConstants.rolesSelectList;
            return View(registrationRequestDto);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Index), "Home");
        }

        private async Task SignInUser(LoginResponseDto loginResponseDto)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(loginResponseDto.Token);
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new(JwtRegisteredClaimNames.Email,
                jwt.Claims.FirstOrDefault(t => t.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new(JwtRegisteredClaimNames.Sub,
                jwt.Claims.FirstOrDefault(t => t.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new(JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(t => t.Type == JwtRegisteredClaimNames.Name).Value));

            // MS Identity claims
            identity.AddClaim(new(ClaimTypes.Name, jwt.Claims.FirstOrDefault(t => t.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new(ClaimTypes.Role, jwt.Claims.FirstOrDefault(t => t.Type == "role").Value));

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}

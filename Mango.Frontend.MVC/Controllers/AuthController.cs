using Mango.Frontend.MVC.Constants;
using Mango.Frontend.MVC.Helper;
using Mango.Frontend.MVC.Models.Dtos;
using Mango.Frontend.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Frontend.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
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

                return RedirectToAction(nameof(Index), "Home");
            }
            else
            {
                ModelState.AddModelError("CustomError", responseDto.Message);
                return View(loginRequestDto);
            }
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
                    TempData["Success"] = "Registration Successful";
                    return RedirectToAction(nameof(Login));
                }

                errorMessage = assignRoleResponse.Message;
            }

            ModelState.AddModelError("CustomError", errorMessage);
            ViewBag.RoleList = RoleConstants.rolesSelectList;
            return View(registrationRequestDto);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }
    }
}

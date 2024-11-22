using Mango.Frontend.MVC.Constants;
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
            LoginRequestDto loginRequestDto = new();
            return View();
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
            }

            TempData["Error"] = registerResponse?.Message;
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

using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Services;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Models
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public Task<UserLoginInfo> Login(LoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            var output = string.Empty;
            var email = registrationRequestDto.Email;

            ApplicationUser user = new()
            {
                UserName= email,
                Email = email,
                NormalizedEmail = email.ToUpper(),
                Name = registrationRequestDto.Name,
                PhoneNumber = registrationRequestDto.PhoneNumber
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _db.ApplicationUsers.First(u => u.UserName == email);
                    var notAvailable = "N/A";

                    UserDto userDto = new()
                    {
                        Id = userToReturn.Id,
                        Name = userToReturn.Name,
                        Email = userToReturn.Email is null ? notAvailable : userToReturn.Email, 
                        PhoneNumber = userToReturn.Email is null ? notAvailable : userToReturn.Email,
                    };

                    return "";
                }
                else
                {
                    var firstError = result.Errors.FirstOrDefault();
                    if (firstError != null)
                    return firstError.Description;
                }
            }
            catch (Exception ex)
            {
            }

            return"Error Encountered";
        }
    }
}

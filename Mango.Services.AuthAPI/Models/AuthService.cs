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
            UserManager<ApplicationUser> _userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = _userManager;
            _roleManager = roleManager;
        }

        public Task<UserLoginInfo> Login(LoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> Register(RegistrationRequestDto registrationRequestDto)
        {
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
                var result = _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.IsCompletedSuccessfully)
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

                    return userDto;
                }
            }
            catch (Exception ex)
            {
            }

            return new UserDto();
        }
    }
}

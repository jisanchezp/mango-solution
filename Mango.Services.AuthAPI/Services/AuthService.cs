using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dto;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignRoleAsync(string email, string roleName)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.NormalizedEmail == email.ToUpper());
            var roleNotExists = await _roleManager.RoleExistsAsync(roleName) == false;
            
            if (user is not null)
            {
                if (roleNotExists)
                {
                    var identityRole = new IdentityRole(roleName);
                    await _roleManager.CreateAsync(identityRole);
                }

                await _userManager.AddToRoleAsync(user, roleName);

                return true;
            }

            return false;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.NormalizedUserName == loginRequestDto.UserName.ToUpper());

            if (user is not null)
            {
                bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (isValid)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var token = _jwtTokenGenerator.GenerateToken(user, roles);

                    UserDto userDto = new()
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.NormalizedEmail,
                        PhoneNumber = user.PhoneNumber
                    };

                    LoginResponseDto loginResponseDto = new()
                    {
                        User = userDto,
                        Token = token
                    };

                    return loginResponseDto;
                }
            }

            return new LoginResponseDto();
        }

        public async Task<string> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            var output = string.Empty;
            var email = registrationRequestDto.Email;

            ApplicationUser user = new()
            {
                UserName = email,
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

            return "Error Encountered";
        }
    }
}

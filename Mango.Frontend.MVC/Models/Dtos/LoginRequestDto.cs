using System.ComponentModel.DataAnnotations;

namespace Mango.Frontend.MVC.Models.Dtos
{
    public class LoginRequestDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}

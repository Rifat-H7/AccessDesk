using System.ComponentModel.DataAnnotations;

namespace AccessDesk_ASP_Server.Models.DTOs.Auth
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Username or email is required")]
        public string UsernameOrEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }
}

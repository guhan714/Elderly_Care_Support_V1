using System.ComponentModel.DataAnnotations;
using ElderlyCareSupport.Server.Enums;

namespace ElderlyCareSupport.Server.Contracts.Login
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "User name is required")]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
            ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "User Type is required")]
        public UsersType UserType { get; set; }
    }
}

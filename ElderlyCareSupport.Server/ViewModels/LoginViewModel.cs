using ElderlyCareSupport.Server.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ElderlyCareSupport.Server.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "User name is required")]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; } = String.Empty;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; } = String.Empty.ToString();

        [Required(ErrorMessage = "User Type is required")]
        public UsersType UserType { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ElderlyCareSupport.Server.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; }
    }
}

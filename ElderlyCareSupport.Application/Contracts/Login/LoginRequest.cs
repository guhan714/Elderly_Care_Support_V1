using System.ComponentModel.DataAnnotations;
using ElderlyCareSupport.Application.Enums;

namespace ElderlyCareSupport.Application.Contracts.Login
{
    public class LoginRequest
    {
        
        public string Email { get; set; } = string.Empty;

       
        public string Password { get; set; } = string.Empty;

        public UsersType UserType { get; set; }
    }
}

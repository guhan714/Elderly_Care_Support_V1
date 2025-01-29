using ElderlyCareSupport.Application.Enums;

namespace ElderlyCareSupport.Application.Contracts.Requests
{
    public class LoginRequest
    {
        
        public string Email { get; set; } = string.Empty;

       
        public string Password { get; set; } = string.Empty;

        public UsersType UserType { get; set; }
    }
}

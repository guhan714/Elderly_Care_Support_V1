using System.ComponentModel.DataAnnotations;
using ElderlyCareSupport.Application.Enums;

namespace ElderlyCareSupport.Application.Contracts
{
    public class RegistrationRequest
    {
        public string FirstName { get; init; } = null!;
        
        public string? LastName { get; set; }
        
        public string Email { get; init; } = null!;
        
        public string Password { get; set; } = null!;
        
        public string ConfirmPassword { get; set; } = null!;


        public long PhoneNumber { get; set; }
        
        public string Gender { get; set; } = null!;

        public string Address { get; set; } = null!;
        
        public string City { get; set; } = null!;
        
        public string Region { get; set; } = null!;

        public long PostalCode { get; set; }
        
        public string Country { get; set; } = null!;
        public UsersType UserType { get; set; }

    }
}

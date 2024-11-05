using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ElderlyCareSupport.Server.ViewModels
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "First Name should not be empty")]
        [MinLength(3)]
        [MaxLength(200)]
        public string FirstName { get; set; } = string.Empty;

        [MinLength(3)]
        [MaxLength(200)]
        public string LastName { get; set; } = string.Empty;

        [MinLength(3)]
        [MaxLength(200)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [MinLength(3)]
        [MaxLength(200)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;    

        public string ConfirmPassword {  get; set; } = string.Empty;
        public long PhoneNumber {  get; set; }

        public string Gender {  get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;    
        public string Region { get; set; } = string.Empty;
        public long PostalCode { get; set; } 
        public string Country { get; set; } = string.Empty;

        public string 


    }
}

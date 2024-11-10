using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ElderlyCareSupport.Server.ViewModels
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = $"{nameof(FirstName)} should not be empty")]
        [MinLength(3)]
        [MaxLength(200)]
        public string FirstName { get; set; } = string.Empty;

        [MinLength(3)]
        [MaxLength(200)]
        public string LastName { get; set; } = string.Empty;

        [MinLength(3)]
        [MaxLength(200)]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = $"{nameof(Email)} should not be empty")]
        public string Email { get; set; } = string.Empty;

        [MinLength(8)]
        [MaxLength(200)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = $"{nameof(Password)} should not be empty")]
        public string Password { get; set; } = string.Empty;

        [MinLength(8)]
        [MaxLength(200)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = $"{nameof(ConfirmPassword)} should not be empty")]
        public string ConfirmPassword {  get; set; } = string.Empty;


        [Required(ErrorMessage = $"{nameof(PhoneNumber)} should not be empty")]
        public long PhoneNumber {  get; set; }

        [MinLength(3)]
        [MaxLength(200)]
        [Required(ErrorMessage = $"{nameof(Gender)} should not be empty")]
        public string Gender {  get; set; } = string.Empty;

        [MaxLength(200)]
        [Required(ErrorMessage = $"{nameof(Address)} should not be empty")]
        public string Address { get; set; } = string.Empty;

        [MaxLength(200)]
        [Required(ErrorMessage = $"{nameof(City)} should not be empty")]
        public string City { get; set; } = string.Empty;

        [MaxLength(200)]
        [Required(ErrorMessage = $"{nameof(Region)} should not be empty")]
        public string Region { get; set; } = string.Empty;

        [Required(ErrorMessage = $"{nameof(PostalCode)} should not be empty")]
        public long PostalCode { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = $"{nameof(Country)} should not be empty")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = $"{nameof(UserType)} should not be empty")]
        public long UserType {  get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace ElderlyCareSupport.Server.ViewModels
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = $"{nameof(FirstName)} should not be empty")]
        [MinLength(3)]
        [MaxLength(200)]
        public string FirstName { get; init; } = null!;

        [MinLength(3)]
        [MaxLength(200)]
        public string? LastName { get; set; }

        [MinLength(3)]
        [MaxLength(200)]
        [DataType(DataType.EmailAddress)]
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        ErrorMessage = "Invalid email address format.")]
        public string Email { get; init; } = null!;

        [MinLength(8)]
        [MaxLength(200)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = $"{nameof(Password)} should not be empty")]
        public string Password { get; set; } = null!;

        [MinLength(8)]
        [MaxLength(200)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = $"{nameof(ConfirmPassword)} should not be empty")]
        public string ConfirmPassword { get; set; } = null!;


        [Required(ErrorMessage = $"{nameof(PhoneNumber)} should not be empty")]
        public long PhoneNumber { get; set; }

        [MinLength(4)]
        [MaxLength(20)]
        [Required(ErrorMessage = $"{nameof(Gender)} should not be empty")]
        public string Gender { get; set; } = null!;

        [MaxLength(200)]
        [Required(ErrorMessage = $"{nameof(Address)} should not be empty")]
        public string Address { get; set; } = null!;

        [MaxLength(200)]
        [Required(ErrorMessage = $"{nameof(City)} should not be empty")]
        public string City { get; set; } = null!;

        [MaxLength(200)]
        [Required(ErrorMessage = $"{nameof(Region)} should not be empty")]
        public string Region { get; set; } = null!;

        [Required(ErrorMessage = $"{nameof(PostalCode)} should not be empty")]
        public long PostalCode { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = $"{nameof(Country)} should not be empty")]
        public string Country { get; set; } = null!;

        [Required(ErrorMessage = $"{nameof(UserType)} should not be empty")]
        public long UserType { get; set; }

    }
}

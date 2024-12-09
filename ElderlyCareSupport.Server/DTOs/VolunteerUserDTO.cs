using ElderlyCareSupport.Server.Models.Enums;

namespace ElderlyCareSupport.Server.DTOs
{
    public class VolunteerUserDto
    {
            public string FirstName { get; } = string.Empty;
            public string? LastName { get; }
            public string Email { get; set; } = string.Empty;
            public string Gender { get; } = string.Empty;
            public string Address { get; } = string.Empty;
            public string City { get; } = string.Empty;
            public string Region { get; } = string.Empty;
            public string Country { get; } = string.Empty;
            public long PhoneNumber { get; set; }
            public long PostalCode { get;}
            public UsersType UserType { get; set; }
    }
}

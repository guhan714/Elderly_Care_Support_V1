namespace ElderlyCareSupport.Application.DTOs
{
    public class ElderUserDto
    {
        public string FirstName { get; init; } = string.Empty;
        public string? LastName { get; init; }
        public string Email { get; init; } = string.Empty;
        public string Gender { get; init; } = string.Empty;
        public string Address { get; init; } = string.Empty;
        public string City { get; init; } = string.Empty;
        public string Region { get; init; } = string.Empty;
        public string Country { get; init; } = string.Empty;
        public long PhoneNumber { get; init; }
        public long PostalCode { get; init; }
        public long UserType { get; init; }

    }
}

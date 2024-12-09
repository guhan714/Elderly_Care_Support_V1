namespace ElderlyCareSupport.Server.Models;

public partial class ElderCareAccount
{
    public long Id { get; init; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string Email { get; init; } = null!;

    public string Password { get; init; } = null!;

    public string ConfirmPassword { get; init; } = null!;

    public long PhoneNumber { get; set; }

    public string Gender { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Region { get; set; } = null!;

    public long PostalCode { get; set; }

    public string Country { get; set; } = null!;

    public long UserType { get; init; }

    public bool? IsActive { get; init; }
}

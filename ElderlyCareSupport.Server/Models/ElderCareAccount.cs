using System;
using System.Collections.Generic;

namespace ElderlyCareSupport.Server.Models;

public partial class ElderCareAccount
{
    public long Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string ConfirmPassword { get; set; } = null!;

    public long PhoneNumber { get; set; }

    public string Gender { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Region { get; set; } = null!;

    public long PostalCode { get; set; }

    public string Country { get; set; } = null!;

    public long UserType { get; set; }
}

namespace ElderlyCareSupport.Domain.Models;

public class LoginCredentials
{
    public string Username { get; set; } = string.Empty;
    
    public string Password { get; set; } = string.Empty;

    public long UserType { get; set; } = 0;
}
using Microsoft.IdentityModel.Tokens;

namespace ElderlyCareSupport.Server.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(string user);
        SecurityTokenDescriptor? ConfigureJwtToken(string userName);
    }
}

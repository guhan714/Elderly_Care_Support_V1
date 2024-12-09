using Microsoft.IdentityModel.Tokens;

namespace ElderlyCareSupport.Server.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string user);
        SecurityTokenDescriptor? ConfigureToken(string userName);
    }
}

using Microsoft.IdentityModel.Tokens;

namespace ElderlyCareSupport.Server.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateJWTToken(string user);
        SecurityTokenDescriptor ConfigureJWTToken(string userName);
    }
}

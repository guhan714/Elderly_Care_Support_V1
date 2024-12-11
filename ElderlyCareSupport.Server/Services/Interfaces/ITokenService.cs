using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.ResponseModels;
using Microsoft.IdentityModel.Tokens;

namespace ElderlyCareSupport.Server.Services.Interfaces
{
    public interface ITokenService
    {
        LoginResponse? GenerateToken();
        Task<string?> ConfigureToken();
    }
}

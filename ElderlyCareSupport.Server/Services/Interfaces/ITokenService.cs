using ElderlyCareSupport.Server.ResponseModels;

namespace ElderlyCareSupport.Server.Services.Interfaces
{
    public interface ITokenService
    {
        Task<LoginResponse?> GenerateToken();
        Task<string?> ConfigureToken();
    }
}

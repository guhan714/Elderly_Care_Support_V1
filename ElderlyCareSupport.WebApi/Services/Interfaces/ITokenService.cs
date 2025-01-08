using ElderlyCareSupport.Server.Contracts;
using ElderlyCareSupport.Server.Contracts.Login;

namespace ElderlyCareSupport.Server.Services.Interfaces
{
    public interface ITokenService
    {
        Task<LoginResponse?> GenerateToken();
        Task<string?> ConfigureToken();
    }
}

using ElderlyCareSupport.Application.Contracts.Response;

namespace ElderlyCareSupport.Application.IService
{
    public interface ITokenService
    {
        Task<LoginResponse?> GenerateToken();
        Task<string?> ConfigureToken();
    }
}

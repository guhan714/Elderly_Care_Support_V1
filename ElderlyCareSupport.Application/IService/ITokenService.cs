using ElderlyCareSupport.Application.Contracts.Login;

namespace ElderlyCareSupport.Application.IService
{
    public interface ITokenService
    {
        Task<LoginResponse?> GenerateToken();
        Task<string?> ConfigureToken();
    }
}

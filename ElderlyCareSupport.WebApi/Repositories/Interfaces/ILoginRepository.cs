using ElderlyCareSupport.Server.Contracts.Login;
using ElderlyCareSupport.Server.ViewModels;

namespace ElderlyCareSupport.Server.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        Task<bool> AuthenticateLogin(LoginRequest loginRequest);
    }
}

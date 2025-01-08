using ElderlyCareSupport.Server.Contracts;
using ElderlyCareSupport.Server.Contracts.Login;
using ElderlyCareSupport.Server.ViewModels;

namespace ElderlyCareSupport.Server.Services.Interfaces
{
    public interface ILoginService
    {
        Task<Tuple<LoginResponse?, bool>> AuthenticateLogin(LoginRequest loginRequest);
    }
}

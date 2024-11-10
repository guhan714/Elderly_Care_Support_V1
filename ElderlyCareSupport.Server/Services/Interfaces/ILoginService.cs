using ElderlyCareSupport.Server.ViewModels;

namespace ElderlyCareSupport.Server.Services.Interfaces
{
    public interface ILoginService
    {
        Task<bool> AuthenticateLogin(LoginViewModel loginViewModel);
    }
}

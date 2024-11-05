using ElderlyCareSupport.Server.ViewModels;

namespace ElderlyCareSupport.Server.Interfaces
{
    public interface ILoginRepository
    {
        Task<bool> AuthenticateLogin(LoginViewModel loginViewModel);
    }
}

using ElderlyCareSupport.Server.ViewModels;

namespace ElderlyCareSupport.Server.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        Task<bool> AuthenticateLogin(LoginViewModel loginViewModel);
    }
}

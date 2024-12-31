using ElderlyCareSupport.Server.ResponseModels;
using ElderlyCareSupport.Server.ViewModels;

namespace ElderlyCareSupport.Server.Services.Interfaces
{
    public interface ILoginService
    {
        Task<Tuple<LoginResponse?, bool>> AuthenticateLogin(LoginViewModel loginViewModel);
    }
}

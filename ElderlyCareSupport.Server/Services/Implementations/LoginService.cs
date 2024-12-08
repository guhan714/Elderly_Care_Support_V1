using ElderlyCareSupport.Server.Repositories.Interfaces;
using ElderlyCareSupport.Server.Services.Interfaces;
using ElderlyCareSupport.Server.ViewModels;

namespace ElderlyCareSupport.Server.Services.Implementations
{
    public class LoginService(ILoginRepository loginRepository, ILogger<LoginService> logger) : ILoginService
    {
        public async Task<bool> AuthenticateLogin(LoginViewModel loginViewModel)
        {
            try
            {
                logger.LogInformation($"Started Login Authentication from {nameof(LoginService)}\nAt Method: {nameof(AuthenticateLogin)}");
                return await loginRepository.AuthenticateLogin(loginViewModel);
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception Occurred in the Login Authentication from {nameof(LoginService)}\nAt Method: {nameof(AuthenticateLogin)}\nException Message: {ex.Message}");
                return await Task.FromResult(false);
            }
        }
    }
}

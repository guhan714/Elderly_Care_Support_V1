using ElderlyCareSupport.Server.Repositories.Interfaces;
using ElderlyCareSupport.Server.Services.Interfaces;
using ElderlyCareSupport.Server.ViewModels;

namespace ElderlyCareSupport.Server.Services.Implementations
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository loginRepository;
        private readonly ILogger<LoginService> logger;
        public LoginService(ILoginRepository _loginRepository, ILogger<LoginService> logger)
        {
            loginRepository = _loginRepository;
            this.logger = logger;
        }

        public async Task<bool> AuthenticateLogin(LoginViewModel loginViewModel)
        {
            try
            {
                logger.LogInformation("Started Login Authentication from {ClassName}\nAt Method: {MethodName}", nameof(LoginService), nameof(AuthenticateLogin));
                var login = await loginRepository.AuthenticateLogin(loginViewModel);
                return login;
            }
            catch (Exception ex)
            {
                logger.LogError("Exception Occurred in the Login Authentication from {ClassName}\nAt Method: {MethodName}\nException Message: {Message}", nameof(LoginService), nameof(AuthenticateLogin), ex.Message);
                return false;
            }
        }
    }
}

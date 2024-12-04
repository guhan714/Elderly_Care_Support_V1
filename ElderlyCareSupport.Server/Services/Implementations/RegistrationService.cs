using ElderlyCareSupport.Server.Repositories.Interfaces;
using ElderlyCareSupport.Server.Services.Interfaces;
using ElderlyCareSupport.Server.ViewModels;

namespace ElderlyCareSupport.Server.Services.Implementations
{
    public class RegistrationService(IRegistrationRepository registrationRepository, ILogger<RegistrationService> logger) : IRegistrationService
    {
        public async Task<bool> checkUserExistingAlready(string email)
        {
            try
            {
                return await registrationRepository.CheckExistingUser(email);
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception Occurred: {ex.Message}");
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> RegisterUserAsync(RegistrationViewModel registrationViewModel)
        {
            try
            {
                var result = await registrationRepository.RegisterUser(registrationViewModel);
                if (result)
                {
                    logger.LogInformation($"Started Registering User Details from {nameof(RegistrationService)} At Method: {nameof(RegisterUserAsync)}");
                    return await Task.FromResult(result);
                }
                else
                {
                    logger.LogWarning($"Can't Register User Details from {nameof(RegistrationService)}\nAt Method: {nameof(RegisterUserAsync)}");
                    return await Task.FromResult(false);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error Registering User Details from {nameof(RegistrationService)}\nAt Method: {nameof(RegisterUserAsync)}\nException: {ex.Message}");
                return await Task.FromResult(false);
            }
        }
    }
}

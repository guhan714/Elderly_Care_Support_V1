using ElderlyCareSupport.Server.Helpers;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using ElderlyCareSupport.Server.Services.Interfaces;
using ElderlyCareSupport.Server.ViewModels;

namespace ElderlyCareSupport.Server.Services.Implementations
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository _registrationRepository;
        private readonly ILogger<RegistrationService> _logger;
        private readonly IEmailService _emailService;
        public RegistrationService(IRegistrationRepository registrationRepository, ILogger<RegistrationService> logger, IEmailService emailService)
        {
            _registrationRepository = registrationRepository;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task<bool> CheckUserExistingAlready(string email)
        {
            try
            {
                return await _registrationRepository.CheckExistingUser(email);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception Occurred: {Message}", ex.Message);
                return false;
            }
        }

        public async Task<bool> RegisterUserAsync(RegistrationRequest registrationRequest)
        {
            try
            {
                registrationRequest.Password = BCryptEncryptionService.EncryptPassword(registrationRequest.Password);
                var result = await _registrationRepository.RegisterUser(registrationRequest);
                if (result)
                {
                    _logger.LogInformation($"Started Registering User Details from {nameof(RegistrationService)} At Method: {nameof(RegisterUserAsync)}");
                    await _emailService.SendEmailAsync(registrationRequest.Email, string.Concat(registrationRequest.FirstName, registrationRequest.LastName));
                    return result;
                }
                _logger.LogWarning($"Can't Register User Details from {nameof(RegistrationService)}\nAt Method: {nameof(RegisterUserAsync)}");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Registering User Details from {nameof(RegistrationService)} At Method: {nameof(RegisterUserAsync)}\nException: {ex.Message}");
                return false;
            }
        }
    }
}

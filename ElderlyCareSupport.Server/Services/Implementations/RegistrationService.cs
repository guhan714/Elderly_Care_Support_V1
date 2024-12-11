﻿using ElderlyCareSupport.Server.Helpers;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using ElderlyCareSupport.Server.Services.Interfaces;
using ElderlyCareSupport.Server.ViewModels;

namespace ElderlyCareSupport.Server.Services.Implementations
{
    public class RegistrationService(IRegistrationRepository registrationRepository, ILogger<RegistrationService> logger, IEmailService emailService) : IRegistrationService
    {
        public async Task<bool> CheckUserExistingAlready(string email)
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
                registrationViewModel.Password = CryptographyHelper.EncryptPassword(registrationViewModel.Password);
                var result = await registrationRepository.RegisterUser(registrationViewModel);
                if (result)
                {
                    logger.LogInformation($"Started Registering User Details from {nameof(RegistrationService)} At Method: {nameof(RegisterUserAsync)}");
                    await emailService.SendEmailAsync(registrationViewModel.Email);
                    return result;
                }
                logger.LogWarning($"Can't Register User Details from {nameof(RegistrationService)}\nAt Method: {nameof(RegisterUserAsync)}");
                return await Task.FromResult(false);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error Registering User Details from {nameof(RegistrationService)}\nAt Method: {nameof(RegisterUserAsync)}\nException: {ex.Message}");
                return await Task.FromResult(false);
            }
        }
    }
}

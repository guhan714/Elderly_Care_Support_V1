using ElderlyCareSupport.Server.ViewModels;
using ElderlyCareSupport.Server.Models;
using ElderlyCareSupport.Server.Helpers;
using System.Security.Cryptography;
using ElderlyCareSupport.Server.Repositories.Interfaces;

namespace ElderlyCareSupport.Server.Repositories.Implementations
{
    public class RegistrationRepository : IRegistrationRepository
    {
        ElderlyCareSupportContext elderlyCareSupportContext;
        ILogger<RegistrationRepository> logger;

        public RegistrationRepository(ElderlyCareSupportContext elderlyCareSupportContext, ILogger<RegistrationRepository> logger)
        {
            this.elderlyCareSupportContext = elderlyCareSupportContext;
            this.logger = logger;
        }

        public async Task<bool> RegisterUser(RegistrationViewModel registrationViewModel)
        {
            try
            {
                var registerModel = MapRegistrationToAccount(registrationViewModel);

                logger.LogInformation("Registration has been started at Class: {ClassName} --> Method: {MethodName}", nameof(RegistrationRepository), nameof(RegisterUser));
                var result = await elderlyCareSupportContext.ElderCareAccounts.AddAsync(registerModel);
                var changes = await elderlyCareSupportContext.SaveChangesAsync();

                if (changes >= 1)
                {
                    logger.LogInformation("Registration successful for user: {Email}", registrationViewModel.Email);
                    return true;
                }
                else
                {
                    logger.LogWarning("Registration failed, no changes were saved for user: {Email}", registrationViewModel.Email);
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Registration has been Failed at Class: {ClassName} --> Method: {MethodName}\nException Occurred: {ExceptionMessage}", nameof(RegistrationRepository), nameof(RegisterUser), ex.Message);
                return false;
            }
        }


        public ElderCareAccount MapRegistrationToAccount(RegistrationViewModel registrationViewModel)
        {
            return new ElderCareAccount
            {
                FirstName = registrationViewModel.FirstName,
                LastName = registrationViewModel.LastName,
                Email = registrationViewModel.Email,
                Address = registrationViewModel.Address,
                PhoneNumber = registrationViewModel.PhoneNumber,
                Password = registrationViewModel.Password,
                ConfirmPassword = registrationViewModel.ConfirmPassword,
                City = registrationViewModel.City,
                Region = registrationViewModel.Region,
                Country = registrationViewModel.Country,
                PostalCode = registrationViewModel.PostalCode,
                UserType = registrationViewModel.UserType,
                Gender = registrationViewModel.Gender
            };
        }
    }
}

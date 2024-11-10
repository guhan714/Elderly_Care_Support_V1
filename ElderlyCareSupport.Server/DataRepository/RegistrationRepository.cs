using ElderlyCareSupport.Server.Interfaces;
using ElderlyCareSupport.Server.ViewModels;
using ElderlyCareSupport.Server.Models;
using ElderlyCareSupport.Server.Helpers;
using System.Security.Cryptography;

namespace ElderlyCareSupport.Server.DataRepository
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
                var registerModel = new ElderCareAccount
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

                var result = await elderlyCareSupportContext.ElderCareAccounts.AddAsync(registerModel);
                await elderlyCareSupportContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex) {

                return false;
            }
        }
    }
}

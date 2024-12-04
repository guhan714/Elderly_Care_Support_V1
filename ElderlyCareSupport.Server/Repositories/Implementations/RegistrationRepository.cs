using ElderlyCareSupport.Server.ViewModels;
using ElderlyCareSupport.Server.Models;
using ElderlyCareSupport.Server.Helpers;
using System.Security.Cryptography;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Contexts;

namespace ElderlyCareSupport.Server.Repositories.Implementations
{
    public class RegistrationRepository(ElderlyCareSupportContext elderlyCareSupportContext, ILogger<RegistrationRepository> logger, IMapper mapper) : IRegistrationRepository
    {
        public async Task<bool> RegisterUser(RegistrationViewModel registrationViewModel)
        {
            try
            {
                var registerModel = mapper.Map<ElderCareAccount>(registrationViewModel);

                logger.LogInformation($"Registration has been started at Class: {nameof(RegistrationRepository)} --> Method: {nameof(RegisterUser)}");
                
                var result = await elderlyCareSupportContext.AddAsync(registerModel);
                
                var changes = await elderlyCareSupportContext.SaveChangesAsync();

                if (changes >= 1)
                {
                    logger.LogInformation($"Registration successful for user: {registrationViewModel.Email}");
                    return (true);
                }
                else
                {
                    logger.LogWarning($"Registration failed, no changes were saved for user: {registrationViewModel.Email}");
                    return (false);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Registration has been Failed at Class: {nameof(RegistrationRepository)} --> Method: {nameof(RegisterUser)}\nException Occurred: {ex.Message}");
                return (false);
            }
        }


        public async Task<bool> CheckExistingUser(string email)
        {
            try
            {
                var isExistingUser = await elderlyCareSupportContext.ElderCareAccounts.FirstOrDefaultAsync(user => user.Email == email)zq                                                                                 ;
                return (isExistingUser is not null);
            }
            catch (Exception ex) 
            {
                logger.LogError($"Exception Occurred. {ex.Message}");
                return (false);
            }
        }
    }
}

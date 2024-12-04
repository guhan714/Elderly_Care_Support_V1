using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Helpers;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using ElderlyCareSupport.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElderlyCareSupport.Server.Services.Implementations
{
    public class VolunteerUserService<T>(ILogger<VolunteerUserDTO> logger, IUserRepository<VolunteerUserDTO> volunteerRepository) : IUserProfileService<T> where T : VolunteerUserDTO, new()
    {

        public async Task<T> GetUserDetails(string emailID)
        {
            T userDetails = new T();
            try
            {
                var result = await RetryHelper.RetryAsync(() => volunteerRepository.GetUserDetailsAsync(emailID), 3, logger);
                return result as T ?? userDetails;
            }
            catch (Exception ex)
            {
                logger.LogError("Exception Occurred : {Exception}.", ex.Message);
                return userDetails;
            }
        }
         
        public async Task<bool> UpdateUserDetails(string emailID, T volunteerCareAccount)
        {
            try
            {
                if (volunteerCareAccount is null)
                    return false;

                var updateResult = await volunteerRepository.UpdateUserDetailsAsync(emailID, volunteerCareAccount);
                return updateResult;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }

        public Task<bool> DeleteUserDetails(string email)
        {
            throw new NotImplementedException();
        }
    }
}

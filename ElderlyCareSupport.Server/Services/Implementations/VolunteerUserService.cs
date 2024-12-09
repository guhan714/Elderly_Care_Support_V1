using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Helpers;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using ElderlyCareSupport.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElderlyCareSupport.Server.Services.Implementations
{
    public class VolunteerUserService<T>(ILogger<VolunteerUserDto> logger, IUserRepository<VolunteerUserDto> volunteerRepository) : IUserProfileService<T> where T : VolunteerUserDto, new()
    {

        public async Task<T?> GetUserDetails(string emailId)
        {
            try
            {
                var result = await RetryHelper.RetryAsync(() => volunteerRepository.GetUserDetailsAsync(emailId), 3, logger);
                return result as T ?? null;
            }
            catch (Exception ex)
            {
                logger.LogError("Exception Occurred : {Exception}.", ex.Message);
                return null;
            }
        }
         
        public async Task<bool> UpdateUserDetails(string emailId, T? volunteerCareAccount)
        {
            try
            {
                if (volunteerCareAccount is null)
                    return false;

                var updateResult = await volunteerRepository.UpdateUserDetailsAsync(emailId, volunteerCareAccount);
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

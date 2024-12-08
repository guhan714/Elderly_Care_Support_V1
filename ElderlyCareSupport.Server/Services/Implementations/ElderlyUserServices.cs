using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Helpers;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using ElderlyCareSupport.Server.Services.Interfaces;

namespace ElderlyCareSupport.Server.Services.Implementations
{
    public class ElderlyUserServices<T>(ILogger<ElderlyUserServices<T>> logger, IUserRepository<ElderUserDto> elderlyUserRepository) : IUserProfileService<T> where T : ElderUserDto, new()
    {
        public async Task<T?> GetUserDetails(string emailId) 
        {
            try
            {
                var result = await RetryHelper.RetryAsync(() => elderlyUserRepository.GetUserDetailsAsync(emailId), 3, logger);
                return result as T;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error Fetching Data {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateUserDetails(string emailId, T? elderUserDto)
        {
            try
            {
                if (elderUserDto is null)
                {
                    return await Task.FromResult(false);
                }

                var result = await elderlyUserRepository.UpdateUserDetailsAsync(emailId,elderUserDto);

                return result;
            }

            catch (Exception)
            {
                return false;
            }
            
        }

        public Task<bool> DeleteUserDetails(string email)
        {
            return Task.FromResult(false);
        }
    }
}

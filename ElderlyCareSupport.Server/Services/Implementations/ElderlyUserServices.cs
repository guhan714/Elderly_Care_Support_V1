using AutoMapper;
using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Helpers;
using ElderlyCareSupport.Server.Models;
using ElderlyCareSupport.Server.Repositories.Implementations;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using ElderlyCareSupport.Server.Services.Interfaces;

namespace ElderlyCareSupport.Server.Services.Implementations
{
    public class ElderlyUserServices<T>(ILogger<ElderlyUserServices<T>> logger, IUserRepository<ElderUserDTO> elderlyUserRepository) : IUserProfileService<T> where T : ElderUserDTO, new()
    {
        public async Task<T> GetUserDetails(string emailID) 
        {
            try
            {
                var result = await RetryHelper.RetryAsync(() => elderlyUserRepository.GetUserDetailsAsync(emailID), 3, logger);
                return result as T ?? new T();
            }
            catch (Exception ex)
            {
                logger.LogError(message: String.Format(format: $"Error Fetching Data {{0}}", arg0: ex.Message));
                return  await Task.FromResult(new T());
            }
        }

        public async Task<bool> UpdateUserDetails(string emailID, T elderUserDTO)
        {
            try
            {
                if (elderUserDTO is null)
                {
                    return await Task.FromResult(false);
                }

                var result = await elderlyUserRepository.UpdateUserDetailsAsync(emailID,elderUserDTO);

                return await Task.FromResult(result ? await Task.FromResult(true) : await Task.FromResult(false));
            }

            catch (Exception)
            {
                return await Task.FromResult(false);
            }
            
        }

        public Task<bool> DeleteUserDetails(string email)
        {
            return Task.FromResult(false);
        }

       
    }
}

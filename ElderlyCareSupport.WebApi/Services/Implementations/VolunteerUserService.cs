using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Helpers;
using ElderlyCareSupport.Server.Models;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using ElderlyCareSupport.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElderlyCareSupport.Server.Services.Implementations
{
    public class VolunteerUserService<T> : IUserProfileService<T> where T : VolunteerUserDto, new()
    {
        private readonly ILogger<VolunteerUserDto> _logger;
        private readonly IUserRepository<VolunteerAccount,VolunteerUserDto> _volunteerRepository;

        public VolunteerUserService(IUserRepository<VolunteerAccount,VolunteerUserDto> volunteerRepository, ILogger<VolunteerUserDto> logger)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
        }

        public async Task<T?> GetUserDetails(string emailId)
        {
            try
            {
                var result = await RetryHelper.RetryAsync(() => _volunteerRepository.GetUserDetailsAsync(emailId), 3, _logger);
                return result as T ?? null;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception Occurred : {Exception}.", ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateUserDetails(string emailId, T? volunteerCareAccount)
        {
            try
            {
                if (volunteerCareAccount is null)
                    return false;

                var updateResult = await _volunteerRepository.UpdateUserDetailsAsync(emailId, volunteerCareAccount);
                return updateResult;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public Task<bool> DeleteUserDetails(string email)
        {
            throw new NotImplementedException();
        }
    }
}

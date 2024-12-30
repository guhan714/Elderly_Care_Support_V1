using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Helpers;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using ElderlyCareSupport.Server.Services.Interfaces;

namespace ElderlyCareSupport.Server.Services.Implementations
{
    public class ElderlyUserServices<T> : IUserProfileService<T> where T : ElderUserDto, new()
    {
        private readonly ILogger<ElderlyUserServices<T>> _logger;
        private readonly IUserRepository<ElderUserDto> _userRepository;

        public ElderlyUserServices(ILogger<ElderlyUserServices<T>> logger, IUserRepository<ElderUserDto> userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<T?> GetUserDetails(string emailId)
        {
            try
            {
                var result = await RetryHelper.RetryAsync(() => _userRepository.GetUserDetailsAsync(emailId), 3, _logger);
                return result as T;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Fetching Data {Message}", ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateUserDetails(string emailId, T? elderUserDto)
        {
            try
            {
                if (elderUserDto is null)
                {
                    return false;
                }

                var result = await _userRepository.UpdateUserDetailsAsync(emailId, elderUserDto);

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

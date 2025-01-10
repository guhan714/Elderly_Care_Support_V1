using ElderlyCareSupport.Application.DTOs;
using ElderlyCareSupport.Application.Helpers;
using ElderlyCareSupport.Application.IRepository;
using ElderlyCareSupport.Application.IService;
using ElderlyCareSupport.Application.Mapping;
using ElderlyCareSupport.Domain.Models;
using Microsoft.Extensions.Logging;

namespace ElderlyCareSupport.Application.Service
{
    public class ElderlyUserServices<T> : IUserProfileService<T> where T : ElderUserDto, new()
    {
        private readonly ILogger<ElderlyUserServices<ElderUserDto>> _logger;
        private readonly IUserRepository<ElderCareAccount, ElderUserDto> _userRepository;

        public ElderlyUserServices(ILogger<ElderlyUserServices<ElderUserDto>> logger, IUserRepository<ElderCareAccount,ElderUserDto> userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<T?> GetUserDetails(string emailId)
        {
            try
            {
                var result = await RetryHelper.RetryAsync(() => _userRepository.GetUserDetailsAsync(emailId), 3, _logger);
                return DomainToDtoMapper.ToElderUserDto(result) as T;
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

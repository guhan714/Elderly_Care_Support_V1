using ElderlyCareSupport.Application.Contracts.Response;
using ElderlyCareSupport.Application.DTOs;
using ElderlyCareSupport.Application.Helpers;
using ElderlyCareSupport.Application.IRepository;
using ElderlyCareSupport.Application.IService;
using ElderlyCareSupport.Application.Mapping;
using ElderlyCareSupport.Domain.Models;

namespace ElderlyCareSupport.Application.Service
{
    using Microsoft.Extensions.Logging;

    public class ElderlyUserServices<T> : IUserProfileService<T>
        where T : ElderUserDto, new()
    {
        private readonly ILogger<ElderlyUserServices<T>> _logger;
        private readonly IUserRepository<ElderCareAccount, ElderUserDto> _userRepository;
        private readonly EmptyModelProvider _emptyModelProvider;
        public ElderlyUserServices(ILogger<ElderlyUserServices<T>> logger, IUserRepository<ElderCareAccount,ElderUserDto> userRepository, EmptyModelProvider emptyModelProvider)
        {
            _logger = logger;
            _userRepository = userRepository;
            _emptyModelProvider = emptyModelProvider;
        }

        public async Task<T?> GetUserDetails(string emailId)
        {
            try
            {
                var result = await RetryHelper.RetryAsync(() => _userRepository.GetUserDetailsAsync(emailId), 3, _logger);
                return MapToDomain.ToElderUserDto(result!) as T ?? _emptyModelProvider.EmptyElderUser as T;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Fetching Data");
                return null;
            }
        }

        public async Task<bool> UpdateUserDetails(string emailId, T? userAccount)
        {
            try
            {
                if (userAccount is null)
                {
                    return false;
                }

                var result = await _userRepository.UpdateUserDetailsAsync(emailId, userAccount);
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

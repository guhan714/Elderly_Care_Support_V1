using ElderlyCareSupport.Application.Contracts.Response;
using ElderlyCareSupport.Application.DTOs;
using ElderlyCareSupport.Application.Helpers;
using ElderlyCareSupport.Application.IRepository;
using ElderlyCareSupport.Application.IService;
using ElderlyCareSupport.Application.Mapping;
using ElderlyCareSupport.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ElderlyCareSupport.Application.Service
{
    public class VolunteerUserService<T> : IUserProfileService<T> 
        where T : VolunteerUserDto, new()
    {
        private readonly ILogger<T> _logger;
        private readonly IUserRepository<VolunteerAccount,VolunteerUserDto> _volunteerRepository;
        private readonly EmptyModelProvider _emptyModelProvider; 
        public VolunteerUserService(IUserRepository<VolunteerAccount,VolunteerUserDto> volunteerRepository, ILogger<T> logger, EmptyModelProvider emptyModelProvider)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
            _emptyModelProvider = emptyModelProvider;
        }

        public async Task<T?> GetUserDetails(string emailId)
        {
            try
            {
                var result = await RetryHelper.RetryAsync(() => _volunteerRepository.GetUserDetailsAsync(emailId), 3, _logger);
                return result is not null ? MapToDomain.ToVolunteerUserDto(result) as T : _emptyModelProvider.EmptyVolunteerUser as T;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occurred : {Exception}.", ex.Message);
                return _emptyModelProvider.EmptyVolunteerUser as T;
            }
        }

        public async Task<bool> UpdateUserDetails(string emailId, T? userAccount)
        {
            try
            {
                if (userAccount is null)
                    return false;

                var updateResult = await _volunteerRepository.UpdateUserDetailsAsync(emailId, userAccount);
                return updateResult;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Message: {Message}", ex.Message);
                return false;
            }
        }

        public Task<bool> DeleteUserDetails(string email)
        {
            throw new NotImplementedException();
        }
    }
}

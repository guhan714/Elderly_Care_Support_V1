using ElderlyCareSupport.Application.Contracts.Common;
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
                return result is not null ? DomainToDtoMapper.ToVolunteerUserDto(result) as T : EmptyModels.EmptyVolunteerUser as T;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception Occurred : {Exception}.", ex.Message);
                return EmptyModels.EmptyVolunteerUser as T;
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

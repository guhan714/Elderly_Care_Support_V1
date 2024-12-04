using AutoMapper;
using ElderlyCareSupport.Server.Contexts;
using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElderlyCareSupport.Server.Repositories.Implementations
{
    public class VolunteerUserRepository<T>(ElderlyCareSupportContext elderlyCareSupportContext, ILogger<T> logger, IMapper mapper) : IUserRepository<T> where T : VolunteerUserDTO, new()
    {
        public Task<bool> DeleteUserDetailsAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetUserDetailsAsync(string emailID)
        {
            T volunteerUser = new();
            try
            {
                var userDetails = await elderlyCareSupportContext.VolunteerAccounts.Where(user => user.Email == emailID).FirstOrDefaultAsync();
                volunteerUser = mapper.Map<T>(userDetails);
                return volunteerUser;
            }
            catch (Exception ex)
            {
                return volunteerUser;
            }
        }

        public Task<bool> UpdateUserDetailsAsync(string emailID, T elderCareAccount)
        {
            throw new NotImplementedException();
        }
    }
}

using AutoMapper;
using ElderlyCareSupport.Server.Contexts;
using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElderlyCareSupport.Server.Repositories.Implementations
{
    public class VolunteerUserRepository<T>(ElderlyCareSupportContext elderlyCareSupportContext, ILogger<T> logger, IMapper mapper) : IUserRepository<T> where T : VolunteerUserDto, new()
    {
        public Task<bool> DeleteUserDetailsAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<T?> GetUserDetailsAsync(string emailId)
        {
            try
            {
                var userDetails = await elderlyCareSupportContext.VolunteerAccounts.FirstOrDefaultAsync(user => user.Email == emailId);
                return mapper.Map<T>(userDetails);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateUserDetailsAsync(string emailId, T elderCareAccount)
        {
            try
            {
                var id = emailId;
                var userDetails = await elderlyCareSupportContext.VolunteerAccounts.FirstOrDefaultAsync(user => user.Email.Equals(id));
                if (userDetails == null)
                    return false;

                userDetails.FirstName = elderCareAccount.FirstName;
                userDetails.LastName = elderCareAccount.LastName;
                userDetails.PhoneNumber = elderCareAccount.PhoneNumber;
                userDetails.Address = elderCareAccount.Address;
                userDetails.City = elderCareAccount.City;
                userDetails.Country = elderCareAccount.Country;
                userDetails.Region = elderCareAccount.Region;
                userDetails.Gender = elderCareAccount.Gender;
                userDetails.PostalCode = elderCareAccount.PostalCode;

                await elderlyCareSupportContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

using AutoMapper;
using ElderlyCareSupport.Server.Contexts;
using ElderlyCareSupport.Server.Controllers;
using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElderlyCareSupport.Server.Repositories.Implementations
{
    public class ElderlyUserRepository<T>(ElderlyCareSupportContext careSupportContext, ILogger<ElderlyUserRepository<T>> logger, IMapper mapper) : IUserRepository<T> where T: ElderUserDto, new()
    {

        public async Task<T?> GetUserDetailsAsync(string emailId)
        {
            try
            {
                var result = await careSupportContext.ElderCareAccounts.Where(user => user.Email == emailId).FirstOrDefaultAsync();
                var elderUserDto = mapper.Map<T>(result);
                logger.LogInformation($"The process has been started to fetch the ElderlyUserDetails... At {nameof(ElderlyUserController)}\tMethod: {nameof(GetUserDetailsAsync)}");
                return elderUserDto;
            }
            catch (Exception ex)
            {
                logger.LogError("Error Occurred During {Method} and Exception: {Message}", nameof(GetUserDetailsAsync), ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateUserDetailsAsync(string emailId, T elderCareAccount)
        {
            try
            {
                var result = await careSupportContext.ElderCareAccounts.FirstOrDefaultAsync(e => e.Email.Equals(emailId));

                if (result is null)
                {
                    return false;
                }

                result.FirstName = elderCareAccount.FirstName;
                result.LastName = elderCareAccount.LastName;
                result.PhoneNumber = elderCareAccount.PhoneNumber;
                result.Address = elderCareAccount.Address;
                result.City = elderCareAccount.City;
                result.Region = elderCareAccount.Region;
                result.Country = elderCareAccount.Country;
                result.PostalCode = elderCareAccount.PostalCode;
                result.Gender = elderCareAccount.Gender;

                await careSupportContext.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                logger.LogError("Error occurred during {MethodName}. Exception: {ExceptionMessage}",
                    nameof(GetUserDetailsAsync), ex.Message);
                return false;
            }
        }

        [ValidateAntiForgeryToken]
        public Task<bool> DeleteUserDetailsAsync(string email)
        {
            try
            {
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return Task.FromResult(ex.InnerException == null);
            }
        }
    }
}

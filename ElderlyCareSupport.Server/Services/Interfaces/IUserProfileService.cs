using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Models;

namespace ElderlyCareSupport.Server.Services.Interfaces
{
    public interface IUserProfileService<T> where T : class
    {
        Task<T> GetUserDetails(string emailID);

        Task<bool> UpdateUserDetails(string emailID, T elderCareAccount);

        Task<bool> DeleteUserDetails(string email);
    }
}

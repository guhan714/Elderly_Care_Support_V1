using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Models;

namespace ElderlyCareSupport.Server.Services.Interfaces
{
    public interface IUserProfileService<T> where T : class
    {
        Task<T?> GetUserDetails(string emailId);

        Task<bool> UpdateUserDetails(string emailId, T? elderCareAccount);

        Task<bool> DeleteUserDetails(string email);
    }
}

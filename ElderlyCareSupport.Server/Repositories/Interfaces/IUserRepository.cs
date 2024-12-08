using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Models;

namespace ElderlyCareSupport.Server.Repositories.Interfaces
{
    public interface IUserRepository<T> where T : class
    {
        Task<T?> GetUserDetailsAsync(string emailId);

        Task<bool> UpdateUserDetailsAsync(string emailId, T elderCareAccount);

        Task<bool> DeleteUserDetailsAsync(string email);
    }
}

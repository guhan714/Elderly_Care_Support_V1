using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Models;

namespace ElderlyCareSupport.Server.Repositories.Interfaces
{
    public interface IUserRepository<T> where T : class
    {
        Task<T> GetUserDetailsAsync(string emailID);

        Task<bool> UpdateUserDetailsAsync(string emailID, T elderCareAccount);

        Task<bool> DeleteUserDetailsAsync(string email);
    }
}

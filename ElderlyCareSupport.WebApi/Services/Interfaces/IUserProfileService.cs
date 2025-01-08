namespace ElderlyCareSupport.Server.Services.Interfaces
{
    public interface IUserProfileService<T> 
    {
        Task<T?> GetUserDetails(string emailId);

        Task<bool> UpdateUserDetails(string emailId, T? elderCareAccount);

        Task<bool> DeleteUserDetails(string email);
    }
}

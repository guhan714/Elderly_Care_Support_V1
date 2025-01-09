namespace ElderlyCareSupport.Application.IRepository
{
    public interface IUserRepository<TReturnObject,TParameter>
    {
        Task<TReturnObject?> GetUserDetailsAsync(string emailId);

        Task<bool> UpdateUserDetailsAsync(string emailId, TParameter elderCareAccount);

        Task<bool> DeleteUserDetailsAsync(string email);
    }
}

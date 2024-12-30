using ElderlyCareSupport.Server.Contexts;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElderlyCareSupport.Server.Repositories.Implementations
{
    public class ForgotPasswordRepository(ElderlyCareSupportContext elderlyCareSupportContext, ILogger<ForgotPasswordRepository> logger) : IForgotPasswordRepository
    {
        public async Task<string?> GetPasswordAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException("Value cannot be null or empty.", nameof(userName));
            try
            {
                var password = await elderlyCareSupportContext.ElderCareAccounts.FirstOrDefaultAsync(user  => user.Email.Equals(userName));
                return password?.Password ?? string.Empty;
            }
            catch (Exception ex)
            {
                logger.LogError("Error Occured during the password retrieval process...At Class {ClassName} Method: {MethodName} ErrorMessage: {Error}",
                                nameof(ForgotPasswordRepository), nameof(GetPasswordAsync), ex.Message);
                return string.Empty;
            }
        }
    }
}

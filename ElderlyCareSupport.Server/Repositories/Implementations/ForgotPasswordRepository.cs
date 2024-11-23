using ElderlyCareSupport.Server.Models;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElderlyCareSupport.Server.Repositories.Implementations
{
    public class ForgotPasswordRepository: IForgotPasswordRepository
    {
        private readonly ElderlyCareSupportContext elderlyCareSupportContext;
        private readonly ILogger<ForgotPasswordRepository> logger;
        public ForgotPasswordRepository(ElderlyCareSupportContext elderlyCareSupportContext, ILogger<ForgotPasswordRepository> logger)
        {
            this.elderlyCareSupportContext = elderlyCareSupportContext;
            this.logger = logger;
        }
        public async Task<string> GetPasswordAsync(string userName)
        {
            try
            {
                var password = await elderlyCareSupportContext.ElderCareAccounts.FirstOrDefaultAsync(user => user.Email == userName);
                return password is null ? String.Empty : password.Password;
            }
            catch (Exception ex)
            {
                logger.LogError("Error Occured during the password retrieval process...At Class {ClassName} Method: {MethodName} ErrorMessage: {Error}", nameof(ForgotPasswordRepository), nameof(GetPasswordAsync), ex.Message);
                return String.Empty;
            }
        }
    }
}

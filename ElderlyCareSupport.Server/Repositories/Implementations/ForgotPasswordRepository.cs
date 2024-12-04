using ElderlyCareSupport.Server.Contexts;
using ElderlyCareSupport.Server.Models;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElderlyCareSupport.Server.Repositories.Implementations
{
    public class ForgotPasswordRepository(ElderlyCareSupportContext elderlyCareSupportContext, ILogger<ForgotPasswordRepository> logger) : IForgotPasswordRepository
    {
        public async Task<string> GetPasswordAsync(string userName)
        {
            try
            {
                string? password = await elderlyCareSupportContext.ElderCareAccounts.Where(e => e.Email == userName).Select(password => password.Password).FirstOrDefaultAsync();
                return await Task.FromResult(password ?? String.Empty);
            }
            catch (Exception ex)
            {
                logger.LogError("Error Occured during the password retrieval process...At Class {ClassName} Method: {MethodName} ErrorMessage: {Error}",
                                nameof(ForgotPasswordRepository), nameof(GetPasswordAsync), ex.Message);
                return await Task.FromResult(String.Empty);
            }
        }
    }
}

using ElderlyCareSupport.Server.Repositories.Interfaces;
using ElderlyCareSupport.Server.Services.Interfaces;

namespace ElderlyCareSupport.Server.Services.Implementations
{
    public class ForgotPasswordService(IForgotPasswordRepository forgotPasswordRepository, ILogger<ForgotPasswordService> logger) : IForgotPaswordService
    {
        public async Task<string?> GetForgotPassword(string userName)
        {
            try
            {
                return await forgotPasswordRepository.GetPasswordAsync(userName) ?? string.Empty;
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception has been occurred in the {nameof(ForgotPasswordService)} Method: {nameof(GetForgotPassword)} Exception: {ex.Message}");
                return string.Empty;
            }
        }
    }
}

using ElderlyCareSupport.Application.IRepository;
using ElderlyCareSupport.Application.IService;
using Microsoft.Extensions.Logging;

namespace ElderlyCareSupport.Application.Service
{
    public class ForgotPasswordService : IForgotPaswordService
    {
        private readonly IForgotPasswordRepository _forgotPasswordRepository;
        private readonly ILogger<ForgotPasswordService> _logger;

        public ForgotPasswordService(IForgotPasswordRepository forgotPasswordRepository, ILogger<ForgotPasswordService> logger)
        {
            _forgotPasswordRepository = forgotPasswordRepository;
            _logger = logger;
        }

        public async Task<string?> GetForgotPassword(string userName)
        {
            try
            {
                return await _forgotPasswordRepository.GetPasswordAsync(userName) ?? string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception has been occurred in the {nameof(ForgotPasswordService)} Method: {nameof(GetForgotPassword)} Exception: {ex.Message}");
                return string.Empty;
            }
        }
    }
}

using ElderlyCareSupport.Application.IRepository;
using ElderlyCareSupport.Application.IService;

namespace ElderlyCareSupport.Application.Service
{
    using Microsoft.Extensions.Logging;

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
                _logger.LogError(ex, "Exception has been occurred in the {Class} Method: {Method} Exception: {Message}", nameof(ForgotPasswordService), nameof(GetForgotPassword), ex);
                return string.Empty;
            }
        }
    }
}

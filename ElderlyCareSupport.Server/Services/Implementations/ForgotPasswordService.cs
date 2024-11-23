using ElderlyCareSupport.Server.Repositories.Interfaces;
using ElderlyCareSupport.Server.Services.Interfaces;

namespace ElderlyCareSupport.Server.Services.Implementations
{
    public class ForgotPasswordService : IForgotPaswordService
    {
        private readonly IForgotPasswordRepository _repository;
        public ForgotPasswordService(IForgotPasswordRepository forgotPasswordRepository)
        {
            this._repository = forgotPasswordRepository;
        }
        public async Task<string> GetForgotPassword(string userName)
        {
            return await _repository.GetPasswordAsync(userName);
        }
    }
}

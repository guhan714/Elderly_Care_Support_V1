using ElderlyCareSupport.Server.Models;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using ElderlyCareSupport.Server.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Web.Http.ModelBinding;

namespace ElderlyCareSupport.Server.Repositories.Implementations
{
    public class LoginRepository : ILoginRepository
    {
        ElderlyCareSupportContext context;
        ILogger<LoginRepository> logger;
        private string decrypted = string.Empty;

        public LoginRepository(ElderlyCareSupportContext elderlyCareSupport, ILogger<LoginRepository> logger)
        {
            context = elderlyCareSupport;
            this.logger = logger;
        }

        public async Task<bool> AuthenticateLogin(LoginViewModel loginViewModel)
        {
            var authenticatedUser = await context.ElderCareAccounts.FirstOrDefaultAsync(t => t.Email == loginViewModel.Email && t.Password == loginViewModel.Password);
            try
            {
                if (authenticatedUser != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception exp)
            {
                logger.LogError($"Exception occurred {exp.Message}.\nClass: {nameof(LoginRepository)}\tMethod: {nameof(AuthenticateLogin)}");
                return false;
            }
        }

    }
}

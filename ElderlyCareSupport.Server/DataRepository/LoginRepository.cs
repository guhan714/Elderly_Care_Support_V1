using ElderlyCareSupport.Server.Interfaces;
using ElderlyCareSupport.Server.Models;
using ElderlyCareSupport.Server.ViewModels;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Web.Http.ModelBinding;

namespace ElderlyCareSupport.Server.DataRepository
{
    public class LoginRepository : ILoginRepository
    {
        ElderlyCareSupportContext context;
        ILogger<LoginRepository> logger;
        private string decrypted = String.Empty;

        public LoginRepository(ElderlyCareSupportContext elderlyCareSupport, ILogger<LoginRepository> logger)
        {
            context = elderlyCareSupport;
            this.logger = logger;
        }

        public async Task<bool> AuthenticateLogin(LoginViewModel loginViewModel)
        {
            var authenticatedUser = context.ElderCareAccounts.Where(t => t.Email == loginViewModel.UserName && t.Password == loginViewModel.Password).FirstOrDefault();
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

using ElderlyCareSupport.Server.Contexts;
using ElderlyCareSupport.Server.Models;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using ElderlyCareSupport.Server.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Web.Http.ModelBinding;

namespace ElderlyCareSupport.Server.Repositories.Implementations
{
    public class LoginRepository(ElderlyCareSupportContext elderlyCareSupport, ILogger<LoginRepository> logger) : ILoginRepository
    {
        public async Task<bool> AuthenticateLogin(LoginViewModel loginViewModel)
        {
            var authenticatedUser = await elderlyCareSupport.ElderCareAccounts.FirstOrDefaultAsync(t => t.Email == loginViewModel.Email && t.Password == loginViewModel.Password && t.UserType == Convert.ToInt64( loginViewModel.UserType));
            try
            {
                if (authenticatedUser != null)
                {
                    return await Task.FromResult(true);
                }
                return await Task.FromResult(false);
            }
            catch (Exception exp)
            {
                logger.LogError($"Exception occurred {exp.Message}.\nClass: {nameof(LoginRepository)}\tMethod: {nameof(AuthenticateLogin)}");
                return await Task.FromResult(false);
            }
        }

    }
}

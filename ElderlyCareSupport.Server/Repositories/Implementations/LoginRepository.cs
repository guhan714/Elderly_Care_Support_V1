﻿using ElderlyCareSupport.Server.Contexts;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using ElderlyCareSupport.Server.ViewModels;
using Microsoft.EntityFrameworkCore;
using ElderlyCareSupport.Server.Helpers;

namespace ElderlyCareSupport.Server.Repositories.Implementations
{
    public class LoginRepository(ElderlyCareSupportContext elderlyCareSupport, ILogger<LoginRepository> logger) : ILoginRepository
    {
        public async Task<bool> AuthenticateLogin(LoginViewModel loginViewModel)
        {
            var userMail = loginViewModel.Email;
            var userType = loginViewModel.UserType;
            var authenticatedUser = await elderlyCareSupport.ElderCareAccounts.FirstOrDefaultAsync(t => t.Email == userMail && t.UserType == Convert.ToInt64(userType));
            var isAuthenticated =
                authenticatedUser?.Password is not null && CryptographyHelper.VerifyPassword(loginViewModel.Password, authenticatedUser.Password);
            try
            {
                return isAuthenticated;
            }
            catch (Exception exp)
            {
                logger.LogError("Exception occurred {Message}.\nClass: {nameof(LoginRepository)}\tMethod: {nameof(AuthenticateLogin)}", exp.Message, nameof(LoginRepository), nameof(AuthenticateLogin));
                return await Task.FromResult(false);
            }
        }

    }
}

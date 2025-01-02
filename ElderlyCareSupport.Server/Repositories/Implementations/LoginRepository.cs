using Dapper;
using ElderlyCareSupport.Server.Helpers;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using ElderlyCareSupport.Server.ViewModels;
using System.Data;
using ElderlyCareSupport.Server.Contexts;
using ElderlyCareSupport.Server.Models;

namespace ElderlyCareSupport.Server.Repositories.Implementations
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ElderlyCareSupportContext _elderlyCareSupport;
        private readonly IDbConnection _dbConnection;
        private readonly ILogger<LoginRepository> _logger;

        public LoginRepository(ElderlyCareSupportContext elderlyCareSupport, ILogger<LoginRepository> logger,
            IDbConnection dbConnection)
        {
            _elderlyCareSupport = elderlyCareSupport;
            _logger = logger;
            _dbConnection = dbConnection;
        }


        public async Task<bool> AuthenticateLogin(LoginViewModel loginViewModel)
        {
            var enumerableHashedPassword = await
                _dbConnection.QueryAsync<string>("""
                                                    SELECT PASSWORD 
                                                    FROM ElderCareAccount 
                                                    WHERE Email = @Email AND UserType = @UserType 
                                                 """, loginViewModel);

            var hashedPassword = enumerableHashedPassword as string[] ?? enumerableHashedPassword.ToArray();
            if (hashedPassword.Length == 0)
            {
                _logger.LogWarning("Invalid login attempt for email: {Email}", loginViewModel.Email);
                return false;
            }

            var isAuthenticated = BCryptEncryptionService.VerifyPassword(loginViewModel.Password, hashedPassword[0]);
            if (!isAuthenticated)
            {
                _logger.LogWarning("Password mismatch for email: {Email}", loginViewModel.Email);
            }

            return isAuthenticated;
        }
    }
}
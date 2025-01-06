using Dapper;
using ElderlyCareSupport.Server.Helpers;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using ElderlyCareSupport.Server.ViewModels;
using System.Data;
using ElderlyCareSupport.Server.Contexts;
using ElderlyCareSupport.Server.Models;
using ElderlyCareSupport.Server.Services.Interfaces;
using MethodTimer;

namespace ElderlyCareSupport.Server.Repositories.Implementations
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IDbConnectionFactory _dbConnection;
        private readonly ILogger<LoginRepository> _logger;

        public LoginRepository( ILogger<LoginRepository> logger,
            IDbConnectionFactory dbConnection)
        {
            _logger = logger;
            _dbConnection = dbConnection;
        }

        [Time]
        public async Task<bool> AuthenticateLogin(LoginViewModel loginViewModel)
        {
            using var connection = _dbConnection.GetConnection();
            var enumerableHashedPassword = await
                connection.QueryAsync<string>("""
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
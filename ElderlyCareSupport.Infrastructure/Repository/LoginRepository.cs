using Dapper;
using ElderlyCareSupport.Application.Contracts.Login;
using ElderlyCareSupport.Application.Helpers;
using ElderlyCareSupport.Application.IRepository;
using ElderlyCareSupport.Application.IService;
using Microsoft.Extensions.Logging;

namespace ElderlyCareSupport.Infrastructure.Repository
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

        public async Task<bool> AuthenticateLogin(LoginRequest loginRequest)
        {
            using var connection = _dbConnection.GetConnection();
            var enumerableHashedPassword = await
                connection.QueryAsync<string>("""
                                                    SELECT PASSWORD 
                                                    FROM ElderCareAccount 
                                                    WHERE Email = @Email AND UserType = @UserType 
                                                 """, loginRequest);

            var hashedPassword = enumerableHashedPassword as string[] ?? enumerableHashedPassword.ToArray();
            if (hashedPassword.Length == 0)
            {
                _logger.LogWarning("Invalid login attempt for email: {Email}", loginRequest.Email);
                return false;
            }

            var isAuthenticated = BCryptEncryptionService.VerifyPassword(loginRequest.Password, hashedPassword[0]);
            if (!isAuthenticated)
            {
                _logger.LogWarning("Password mismatch for email: {Email}", loginRequest.Email);
            }

            return isAuthenticated;
        }
    }
}
using Dapper;
using ElderlyCareSupport.Application.Contracts.Requests;
using ElderlyCareSupport.Application.Helpers;
using ElderlyCareSupport.Application.IRepository;
using ElderlyCareSupport.Application.IService;
using ElderlyCareSupport.SQL;
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

        public async Task<bool> AuthenticateLogin(LoginRequest loginCredentials)
        {
            using var connection = _dbConnection.GetConnection();
            connection.Open();
            var enumerableHashedPassword = await
                connection.QueryAsync<string>(AuthenticationQueries.LoginQuery, loginCredentials);

            var hashedPassword = enumerableHashedPassword as string[] ?? enumerableHashedPassword.ToArray();
            if (hashedPassword.Length == 0)
            {
                _logger.LogWarning("Invalid login attempt for email: {Email}", loginCredentials.Email);
                return false;
            }

            var isAuthenticated = BCryptEncryptionService.VerifyPassword(loginCredentials.Password, hashedPassword[0]);
            if (!isAuthenticated)
            {
                _logger.LogWarning("Password mismatch for email: {Email}", loginCredentials.Email);
            }

            return isAuthenticated;
        }
    }
}
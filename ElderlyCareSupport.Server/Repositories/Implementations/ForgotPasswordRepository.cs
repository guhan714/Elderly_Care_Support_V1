using Dapper;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using System.Data;
using ElderlyCareSupport.Server.Contexts;
using ElderlyCareSupport.Server.Models;
using ElderlyCareSupport.Server.Services.Interfaces;

namespace ElderlyCareSupport.Server.Repositories.Implementations
{
    public class ForgotPasswordRepository : IForgotPasswordRepository
    {
        private readonly ILogger<ForgotPasswordRepository> _logger;
        private readonly IDbConnectionFactory _dbConnection;

        public ForgotPasswordRepository( 
            ILogger<ForgotPasswordRepository> logger, IDbConnectionFactory dbConnection)
        {
            _logger = logger;
            _dbConnection = dbConnection;
        }

        public async Task<string?> GetPasswordAsync(string userName)
        {
            try
            {
                using var connection = _dbConnection.GetConnection();
                var password = await connection.QueryFirstOrDefaultAsync<string>("""
                    SELECT Password FROM ElderCareAccount WHERE Email = @userName
                    """, new { userName });
                return password ?? string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error Occured during the password retrieval process...At Class {ClassName} Method: {MethodName} ErrorMessage: {Error}",
                    nameof(ForgotPasswordRepository), nameof(GetPasswordAsync), ex.Message);
                return string.Empty;
            }
        }
    }
}
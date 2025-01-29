using Dapper;
using ElderlyCareSupport.Application.IRepository;
using ElderlyCareSupport.Application.IService;
using ElderlyCareSupport.SQL;
using Microsoft.Extensions.Logging;

namespace ElderlyCareSupport.Infrastructure.Repository
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
                connection.Open();
                var password = await connection.QueryFirstOrDefaultAsync<string>(AuthenticationQueries.ForgotPasswordQuery, new {userName});
                return password ?? string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, 
                    "Error Occured during the password retrieval process...At Class {ClassName} Method: {MethodName} ErrorMessage: {Error}",
                    nameof(ForgotPasswordRepository), nameof(GetPasswordAsync), ex.Message);
                return string.Empty;
            }
        }
    }
}
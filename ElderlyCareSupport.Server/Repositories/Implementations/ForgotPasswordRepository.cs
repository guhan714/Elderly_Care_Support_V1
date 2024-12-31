using Dapper;
using ElderlyCareSupport.Server.Contexts;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using System.Data;

namespace ElderlyCareSupport.Server.Repositories.Implementations
{
    public class ForgotPasswordRepository : IForgotPasswordRepository
    {
        private readonly ElderlyCareSupportContext _elderlyCareSupportContext;
        private readonly ILogger<ForgotPasswordRepository> _logger;
        private readonly IDbConnection _dbConnection;

        public ForgotPasswordRepository(ElderlyCareSupportContext elderlyCareSupportContext,
            ILogger<ForgotPasswordRepository> logger, IDbConnection dbConnection)
        {
            this._elderlyCareSupportContext = elderlyCareSupportContext;
            this._logger = logger;
            _dbConnection = dbConnection;
        }

        public async Task<string?> GetPasswordAsync(string userName)
        {
            try
            {
                var password = await _dbConnection.QueryFirstOrDefaultAsync<string>("""
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
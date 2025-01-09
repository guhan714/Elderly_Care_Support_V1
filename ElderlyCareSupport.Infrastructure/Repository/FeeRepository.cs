using Dapper;
using ElderlyCareSupport.Application.IRepository;
using ElderlyCareSupport.Application.IService;
using ElderlyCareSupport.Domain.Models;
using Microsoft.Extensions.Logging;

namespace ElderlyCareSupport.Infrastructure.Repository
{
    public class FeeRepository : IFeeRepository
    {
        private readonly IDbConnectionFactory _dbConnection;
        private readonly ILogger<FeeRepository> _logger;

        public FeeRepository( ILogger<FeeRepository> logger, IDbConnectionFactory dbConnection)
        {
            _logger = logger;
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<FeeConfiguration>> GetAllFeeDetailsAsync()
        {
            try
            {
                using var connection = _dbConnection.GetConnection();
                _logger.LogInformation("Data Fetching Started:  class: {Class} Method: {Method}", nameof(FeeRepository), nameof(GetAllFeeDetailsAsync));
                var result = await
                    connection.QueryAsync<FeeConfiguration>("""
                               SELECT
                               FEE_ID AS FeeId, FEE_NAME AS FeeName, FEE_AMOUNT AS FeeAmount, Description as description
                               FROM dbo.FEE_CONFIGURATION;
                            """);
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured:  class: {Class} Method: {Method}\nMessage: {Ex}", nameof(FeeRepository), nameof(GetAllFeeDetailsAsync), ex.Message);
                return [];
            }
        }

    }
}

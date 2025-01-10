using Dapper;
using ElderlyCareSupport.Application.IRepository;
using ElderlyCareSupport.Application.IService;
using ElderlyCareSupport.Domain.Models;
using ElderlyCareSupport.SQL;
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

        public async Task<IReadOnlyList<FeeConfiguration>> GetAllFeeDetailsAsync()
        {
            try
            {
                using var connection = _dbConnection.GetConnection();
                connection.Open();
                _logger.LogInformation("Data Fetching Started:  class: {Class} Method: {Method}", nameof(FeeRepository), nameof(GetAllFeeDetailsAsync));
                var result = await
                    connection.QueryAsync<FeeConfiguration>(AuthenticationQueries.AllFeeDetailsQuery);
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

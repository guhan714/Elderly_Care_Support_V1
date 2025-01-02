using AutoMapper;
using Dapper;
using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Helpers;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using System.Data;
using ElderlyCareSupport.Server.Contexts;

namespace ElderlyCareSupport.Server.Repositories.Implementations
{
    public class FeeRepository : IFeeRepository
    {
        private readonly ElderlyCareSupportContext _context;
        private readonly IDbConnection _dbConnection;
        private readonly ILogger<FeeRepository> _logger;
        private readonly IMapper _mapper;

        public FeeRepository(ElderlyCareSupportContext context, ILogger<FeeRepository> logger, IMapper mapper, IDbConnection dbConnection)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<FeeConfigurationDto>> GetAllFeeDetailsAsync()
        {
            try
            {
                _logger.LogInformation("Data Fetching Started:  class: {Class} Method: {Method}", nameof(FeeRepository), nameof(GetAllFeeDetailsAsync));
                var result = await
                    _dbConnection.QueryAsync<dynamic>("""
                               SELECT
                               FEE_ID AS FeeId, FEE_NAME AS FeeName, FEE_AMOUNT AS FeeAmount, Description as description
                               FROM dbo.FEE_CONFIGURATION;
                            """);
                return DomainToDtoMapper.ToFeeConfigurationDto(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured:  class: {Class} Method: {Method}\nMessage: {Ex}", nameof(FeeRepository), nameof(GetAllFeeDetailsAsync), ex.Message);
                return [];
            }
        }

    }
}

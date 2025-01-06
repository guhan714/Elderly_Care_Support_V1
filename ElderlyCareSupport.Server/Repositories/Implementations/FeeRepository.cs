﻿using AutoMapper;
using Dapper;
using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Helpers;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using System.Data;
using ElderlyCareSupport.Server.Contexts;
using ElderlyCareSupport.Server.Models;
using ElderlyCareSupport.Server.Services.Interfaces;
using MethodTimer;

namespace ElderlyCareSupport.Server.Repositories.Implementations
{
    public class FeeRepository : IFeeRepository
    {
        private readonly IDbConnectionFactory _dbConnection;
        private readonly ILogger<FeeRepository> _logger;
        private readonly IMapper _mapper;

        public FeeRepository( ILogger<FeeRepository> logger, IMapper mapper, IDbConnectionFactory dbConnection)
        {
            _logger = logger;
            _mapper = mapper;
            _dbConnection = dbConnection;
        }

        [Time]
        public async Task<IEnumerable<FeeConfigurationDto>> GetAllFeeDetailsAsync()
        {
            try
            {
                using var connection = _dbConnection.GetConnection();
                _logger.LogInformation("Data Fetching Started:  class: {Class} Method: {Method}", nameof(FeeRepository), nameof(GetAllFeeDetailsAsync));
                var result = await
                    connection.QueryAsync<FeeConfigurationDto>("""
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

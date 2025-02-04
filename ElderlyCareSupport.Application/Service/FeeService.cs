﻿using ElderlyCareSupport.Application.DTOs;
using ElderlyCareSupport.Application.IRepository;
using ElderlyCareSupport.Application.IService;
using ElderlyCareSupport.Application.Mapping;
using Microsoft.Extensions.Logging;

namespace ElderlyCareSupport.Application.Service
{
    public class FeeService : IFeeService
    {
        private readonly IFeeRepository _feeRepository;
        private readonly ILogger<FeeService> _logger;

        public FeeService(ILogger<FeeService> logger, IFeeRepository feeRepository)
        {
            _logger = logger;
            _feeRepository = feeRepository;
        }

        public async Task<List<FeeConfigurationDto>> GetAllFeeDetails()
        {
            try
            {
                var feeDetails = await _feeRepository.GetAllFeeDetailsAsync();  
                var feeConfigurations = feeDetails.ToList();
                if (feeConfigurations.Count == 0)
                {
                    _logger.LogWarning("Can't Fetch Fee Details from {ServiceName}\nAt Method: {MethodName}", nameof(FeeService), nameof(GetAllFeeDetails));
                }
                _logger.LogInformation($"Started Fetching Fee Details from {nameof(FeeService)}\nAt Method: {nameof(GetAllFeeDetails)}");
                return MapToDomain.ToFeeConfigurationDto(feeConfigurations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Fetching Fee Details from {Class}\nAt Method: {Method}\nException Message: {Message}", nameof(FeeService), nameof(GetAllFeeDetails), ex);
                return [];
            }
        }
    }
}

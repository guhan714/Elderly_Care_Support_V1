using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Helpers;
using ElderlyCareSupport.Server.Models;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using ElderlyCareSupport.Server.Services.Interfaces;
using MethodTimer;

namespace ElderlyCareSupport.Server.Services.Implementations
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
                var feeConfigurations = feeDetails.ToArray();
                if (feeConfigurations.Length == 0)
                {
                    _logger.LogWarning("Can't Fetch Fee Details from {ServiceName}\nAt Method: {MethodName}", nameof(FeeService), nameof(GetAllFeeDetails));
                }
                _logger.LogInformation($"Started Fetching Fee Details from {nameof(FeeService)}\nAt Method: {nameof(GetAllFeeDetails)}");
                return DomainToDtoMapper.ToFeeConfigurationDto(feeConfigurations).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Fetching Fee Details from {Class}\nAt Method: {Method}\nException Message: {Message}", nameof(FeeService), nameof(GetAllFeeDetails), ex.Message);
                return [];
            }
        }
    }
}

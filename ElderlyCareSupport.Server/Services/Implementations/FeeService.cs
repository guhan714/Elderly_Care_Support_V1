using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using ElderlyCareSupport.Server.Services.Interfaces;

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

        public async Task<IEnumerable<FeeConfigurationDto>> GetAllFeeDetails()
        {
            try
            {
                var feeDetails = await _feeRepository.GetAllFeeDetailsAsync();
                feeDetails = feeDetails.ToArray();
                if (feeDetails.Any())
                {
                    _logger.LogInformation($"Started Fetching Fee Details from {nameof(FeeService)}\nAt Method: {nameof(GetAllFeeDetails)}");
                    return feeDetails;
                }
                _logger.LogWarning($"Can't Fetch Fee Details from {nameof(FeeService)}\nAt Method: {nameof(GetAllFeeDetails)}");
                return feeDetails;
            }
            catch (Exception ex)
            {
                _logger.LogError(@"Error Fetching Fee Details from {Class}\nAt Method: {Method}\nException Message: {Message}", nameof(FeeService), nameof(GetAllFeeDetails), ex.Message);
                return [];
            }
        }
    }
}

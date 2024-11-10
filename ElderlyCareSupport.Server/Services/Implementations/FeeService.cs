using ElderlyCareSupport.Server.Models;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using ElderlyCareSupport.Server.Services.Interfaces;

namespace ElderlyCareSupport.Server.Services.Implementations
{
    public class FeeService : IFeeService
    {
        private readonly IFeeRepository _feeRepository;
        private readonly ILogger<FeeService> _logger;
        public FeeService(IFeeRepository feeRepository, ILogger<FeeService> logger)
        {
            _feeRepository = feeRepository;
            _logger = logger;   
        }

        public async Task<List<FeeConfiguration>> GetAllFeeDetails()
        {
            try
            {
                var feeDetails = await _feeRepository.GetAllFeeDetails();
                if (feeDetails.Count >= 1)
                {
                    _logger.LogInformation("Started Fetching Fee Details from {ClassName}\nAt Method: {MethodName}", nameof(FeeService), nameof(GetAllFeeDetails));
                    return feeDetails;
                }
                else
                {
                    _logger.LogWarning("Can't Fetch Fee Details from {ClassName}\nAt Method: {MethodName}", nameof(FeeService), nameof(GetAllFeeDetails));
                    return new List<FeeConfiguration>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Fetching Fee Details from {ClassName}\nAt Method: {MethodName}\nException Message: {Message}", nameof(FeeService), nameof(GetAllFeeDetails), ex.Message);
                return new List<FeeConfiguration>();
            }
        }
    }
}

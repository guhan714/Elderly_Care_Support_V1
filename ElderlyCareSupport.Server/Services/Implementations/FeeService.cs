using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Models;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using ElderlyCareSupport.Server.Services.Interfaces;

namespace ElderlyCareSupport.Server.Services.Implementations
{
    public class FeeService(IFeeRepository feeRepository, ILogger<FeeService> logger) : IFeeService
    {
        public async Task<IEnumerable<FeeConfigurationDTO>> GetAllFeeDetails()
        {
            try
            {
                 var feeDetails = await feeRepository.GetAllFeeDetailsAsync();
                if (feeDetails.Any())
                {
                    logger.LogInformation($"Started Fetching Fee Details from {nameof(FeeService)}\nAt Method: {nameof(GetAllFeeDetails)}");
                    return feeDetails;
                }
                else
                {
                    logger.LogWarning($"Can't Fetch Fee Details from {nameof(FeeService)}\nAt Method: {nameof(GetAllFeeDetails)}");
                    return feeDetails;
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error Fetching Fee Details from {nameof(FeeService)}\nAt Method: {nameof(GetAllFeeDetails)}\nException Message: {ex.Message}");
                return [];
            }
        }
    }
}

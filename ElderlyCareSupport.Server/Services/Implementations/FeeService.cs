using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using ElderlyCareSupport.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ElderlyCareSupport.Server.Services.Implementations
{
    public class FeeService(IFeeRepository feeRepository, ILogger<FeeService> logger) : IFeeService
    {
        public async Task<List<FeeConfigurationDto>> GetAllFeeDetails()
        {
            try
            {
                 var feeDetails = await feeRepository.GetAllFeeDetailsAsync();
                 if (feeDetails.Count != 0)
                 {
                     logger.LogInformation($"Started Fetching Fee Details from {nameof(FeeService)}\nAt Method: {nameof(GetAllFeeDetails)}");
                     return feeDetails;
                 }
                 logger.LogWarning($"Can't Fetch Fee Details from {nameof(FeeService)}\nAt Method: {nameof(GetAllFeeDetails)}");
                 return feeDetails;
            }
            catch (Exception ex)
            {
                logger.LogError(@"Error Fetching Fee Details from {Class}\nAt Method: {Method}\nException Message: {Message}", nameof(FeeService), nameof(GetAllFeeDetails), ex.Message);
                return [];
            }
        }
    }
}

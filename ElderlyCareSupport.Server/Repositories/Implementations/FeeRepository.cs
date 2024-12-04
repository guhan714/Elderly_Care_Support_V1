using AutoMapper;
using ElderlyCareSupport.Server.Contexts;
using ElderlyCareSupport.Server.Controllers;
using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Models;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElderlyCareSupport.Server.Repositories.Implementations
{
    public class FeeRepository(ElderlyCareSupportContext context, ILogger<FeeRepository> logger, IMapper mapper) : IFeeRepository
    {
        public async Task<IEnumerable<FeeConfigurationDTO>> GetAllFeeDetailsAsync()
        {
            try
            {
                logger.LogInformation($"Data Fetching Started:  class: {nameof(FeeRepository)} Method: {GetAllFeeDetailsAsync}");
                var result =  await context.FeeConfigurations.ToArrayAsync();
                return mapper.Map<IEnumerable<FeeConfigurationDTO>>(result);
            }
            catch (Exception ex)
            {
                logger.LogError(message: $"Exception occured:  class: {nameof(FeeRepository)} Method: {nameof(GetAllFeeDetailsAsync)}\nMessage: {ex.Message}");
                return Array.Empty<FeeConfigurationDTO>();
            }
        }

    }
}

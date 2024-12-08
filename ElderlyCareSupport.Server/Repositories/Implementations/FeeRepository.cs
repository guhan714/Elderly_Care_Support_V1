using AutoMapper;
using ElderlyCareSupport.Server.Contexts;
using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElderlyCareSupport.Server.Repositories.Implementations
{
    public class FeeRepository(ElderlyCareSupportContext context, ILogger<FeeRepository> logger, IMapper mapper) : IFeeRepository
    {
        public async Task<List<FeeConfigurationDto>> GetAllFeeDetailsAsync()
        {
            try
            {
                logger.LogInformation("Data Fetching Started:  class: {Class} Method: {Method}", nameof(FeeRepository), nameof(GetAllFeeDetailsAsync));
                var result =  await context.FeeConfigurations.ToListAsync();
                return mapper.Map<List<FeeConfigurationDto>>(result);
            }
            catch (Exception ex)
            {
                logger.LogError(@"Exception occured:  class: {Class} Method: {Method}\nMessage: {Ex}",nameof(FeeRepository),nameof(GetAllFeeDetailsAsync), ex.Message);
                return [];
            }
        }

    }
}

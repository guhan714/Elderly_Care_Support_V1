using ElderlyCareSupport.Server.Controllers;
using ElderlyCareSupport.Server.Models;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElderlyCareSupport.Server.Repositories.Implementations
{
    public class FeeRepository : IFeeRepository
    {
        ElderlyCareSupportContext context;
        ILogger<FeeRepository> logger;
        public FeeRepository(ElderlyCareSupportContext context, ILogger<FeeRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<List<FeeConfiguration>> GetAllFeeDetails()
        {
            try
            {
                logger.LogInformation("Data Fetching Started:  class: {ClassName} Method: {MethodName}", nameof(ElderlyCareHomeController), nameof(GetAllFeeDetails));
                return await context.FeeConfigurations.ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError("Exception occured:  class: {ClassName} Method: {MethodName}\nMessage: {ExceptionMessage}", nameof(ElderlyCareHomeController), nameof(GetAllFeeDetails), ex.Message);
                return new List<FeeConfiguration>();
            }
        }

    }
}

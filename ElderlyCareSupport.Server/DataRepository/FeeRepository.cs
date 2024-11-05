using ElderlyCareSupport.Server.Controllers;
using ElderlyCareSupport.Server.HelperInterface;
using ElderlyCareSupport.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace ElderlyCareSupport.Server.DataRepository
{
    public class FeeRepository:IFeeRepository
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
                logger.LogInformation($"Data Fetching Started:  class: {nameof(ElderlyCareHomeController)} Method: {nameof(GetAllFeeDetails)}");
                return context.FeeConfigurations.ToList();
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception occured:  class: {nameof(ElderlyCareHomeController)} Method: {nameof(GetAllFeeDetails)}\nMessage: {ex.Message}");
                return new List<FeeConfiguration>();
            }
        }

    }
}

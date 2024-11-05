using ElderlyCareSupport.Server.HelperInterface;
using ElderlyCareSupport.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace ElderlyCareSupport.Server.DataRepository
{
    public class FeeRepository:IFeeRepository
    {
        ElderlyCareSupportContext context;
        public FeeRepository(ElderlyCareSupportContext context)
        {
            this.context = context;
        }

        public async Task<List<FeeConfiguration>> GetAllFeeDetails()
        {
            return context.FeeConfigurations.ToList();
        }

    }
}

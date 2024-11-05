using ElderlyCareSupport.Server.Models;

namespace ElderlyCareSupport.Server.HelperInterface
{
    public interface IFeeRepository
    {
        Task<List<FeeConfiguration>> GetAllFeeDetails();
    }
}

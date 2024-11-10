using ElderlyCareSupport.Server.Models;

namespace ElderlyCareSupport.Server.Services.Interfaces
{
    public interface IFeeService
    {
        Task<List<FeeConfiguration>> GetAllFeeDetails();
    }
}

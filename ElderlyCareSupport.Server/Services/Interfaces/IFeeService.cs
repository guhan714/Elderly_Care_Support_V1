using ElderlyCareSupport.Server.DTOs;

namespace ElderlyCareSupport.Server.Services.Interfaces
{
    public interface IFeeService
    {
        Task<List<FeeConfigurationDto>> GetAllFeeDetails();
    }
}

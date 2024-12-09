using ElderlyCareSupport.Server.DTOs;

namespace ElderlyCareSupport.Server.Repositories.Interfaces
{
    public interface IFeeRepository
    {
        Task<List<FeeConfigurationDto>> GetAllFeeDetailsAsync();
    }
}

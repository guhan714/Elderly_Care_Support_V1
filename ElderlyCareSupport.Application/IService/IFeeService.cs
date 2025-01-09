using ElderlyCareSupport.Application.DTOs;

namespace ElderlyCareSupport.Application.IService
{
    public interface IFeeService
    {
        Task<List<FeeConfigurationDto>> GetAllFeeDetails();
    }
}

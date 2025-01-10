using ElderlyCareSupport.Domain.Models;

namespace ElderlyCareSupport.Application.IRepository
{
    public interface IFeeRepository
    {
        Task<IReadOnlyList<FeeConfiguration>> GetAllFeeDetailsAsync();
    }
}

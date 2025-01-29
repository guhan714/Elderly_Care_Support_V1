using ElderlyCareSupport.Domain.Models;

namespace ElderlyCareSupport.Application.IRepository;

public interface ITaskMasterRepository
{
    Task<List<TaskCategory>> GetTaskCategories();
}
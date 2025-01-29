using ElderlyCareSupport.Domain.Models;

namespace ElderlyCareSupport.Application.IService;

public interface ITaskMaster
{
    Task<List<TaskCategory>> GetTaskCategories();
}
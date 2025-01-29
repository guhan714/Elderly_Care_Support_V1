using ElderlyCareSupport.Application.IRepository;
using ElderlyCareSupport.Application.IService;
using ElderlyCareSupport.Domain.Models;

namespace ElderlyCareSupport.Application.Service;

public class TaskCategoryService : ITaskMaster
{
    private readonly ITaskMasterRepository _taskMasterRepository;

    public TaskCategoryService(ITaskMasterRepository taskMasterRepository)
    {
        _taskMasterRepository = taskMasterRepository;
    }

    public async Task<List<TaskCategory>> GetTaskCategories()
    {
        var taskCategories = await _taskMasterRepository.GetTaskCategories();
        return taskCategories;
    }
}
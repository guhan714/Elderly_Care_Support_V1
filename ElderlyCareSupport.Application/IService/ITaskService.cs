using ElderlyCareSupport.Application.Contracts.Requests;
using ElderlyCareSupport.Domain.Models;

namespace ElderlyCareSupport.Application.IService;

public interface ITaskService
{
    Task<bool> CreateTask(TaskCreationRequest task);
    Task<Tuple<TaskDetails, bool>> UpdateTask(TaskCreationRequest task);
    Task<Tuple<TaskDetails, bool>> CancelTask(TaskCreationRequest task);
    Task<List<TaskDetails>?> GetTasks(long userId, TaskQueryParameters taskQueryParameters);
    
}
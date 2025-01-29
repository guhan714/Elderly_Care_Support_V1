using ElderlyCareSupport.Application.Contracts.Requests;
using ElderlyCareSupport.Application.IRepository;
using ElderlyCareSupport.Application.IService;
using ElderlyCareSupport.Domain.Models;
using FluentValidation;

namespace ElderlyCareSupport.Application.Service;

public class TaskService : ITaskService
{
    
    private readonly ITaskRepository _repository;
    

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }


    public async Task<bool> CreateTask(TaskCreationRequest task)
    {
        var createdTask = await _repository.CreateTask(task);
        return createdTask;
    }

    public async Task<Tuple<TaskDetails,bool>> UpdateTask(TaskCreationRequest task)
    {
        var updatedTask = await _repository.UpdateTask(task);
        return updatedTask;
    }

    public async Task<Tuple<TaskDetails, bool>> CancelTask(TaskCreationRequest task)
    {
        throw new NotImplementedException();
    }

    public async Task<List<TaskDetails>?> GetTasks(long userId, TaskQueryParameters taskQueryParameters)
    {
        var results = await _repository.GetTasks(userId, taskQueryParameters);
        return results ?? Enumerable.Empty<TaskDetails>().ToList();
    }
    
}
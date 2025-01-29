using Dapper;
using ElderlyCareSupport.Application.Contracts.Requests;
using ElderlyCareSupport.Application.IRepository;
using ElderlyCareSupport.Application.IService;
using ElderlyCareSupport.Domain.Models;
using ElderlyCareSupport.Domain.ValueObjects;
using ElderlyCareSupport.SQL;

namespace ElderlyCareSupport.Infrastructure.Repository;

public class TaskRepository : ITaskRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public TaskRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<bool> CreateTask(TaskCreationRequest task)
    {
        var connection = _dbConnectionFactory.GetConnection();
        var result = await connection.ExecuteAsync(TaskQueries.CreateTaskQuery, new
        {
            TaskName = task.TaskName,
            TaskDescription = task.TaskDescription,
            StartDate = task.StartDate,
            EndDate = task.EndDate,
            TaskStatusId = 1,
            ElderlyPersonId = task.ElderlyId,
            CreatedDate = task.CreationDate,
            UpdatedDate = task.ModificationDate
        });
        return result > 0;
    }

    public async Task<Tuple<TaskDetails, bool>> UpdateTask(TaskCreationRequest task)
    {
        throw new NotImplementedException();
    }

    public async Task<Tuple<TaskDetails, bool>> CancelTask(TaskCreationRequest task)
    {
        throw new NotImplementedException();
    }

    public async Task<List<TaskDetails>?> GetTasks(long userId, TaskQueryParameters taskQueryParameters)
    {
        var connection = _dbConnectionFactory.GetConnection();
        var result =
            await connection.QueryAsync<TaskDetails>(TaskQueries.GetTaskById, new { userId, taskQueryParameters });
        return result.ToList();
    }


}
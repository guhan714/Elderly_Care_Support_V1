using Dapper;
using ElderlyCareSupport.Application.IRepository;
using ElderlyCareSupport.Application.IService;
using ElderlyCareSupport.Domain.Models;
using ElderlyCareSupport.SQL;

namespace ElderlyCareSupport.Infrastructure.Repository;

public class TaskAssignmentRepository : IAssignTaskRepository
{

    private readonly IDbConnectionFactory _connectionFactory;

    public TaskAssignmentRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<List<TaskDetails>> GetUnAssignedTasks()
    {
        using var connection = _connectionFactory.GetConnection();
        var unAssignedTasks = await connection.QueryAsync<TaskDetails>(TaskQueries.GetUnAssignedTask, new { IsAssigned = 0 });
        return unAssignedTasks.ToList();
    }

    public async Task<string> GetPreferredGender(int genderId)
    {
        using var connection = _connectionFactory.GetConnection();
        var preferredGender = await connection.QueryAsync<string>(TaskQueries.GetGenderById, new { GenderId = genderId });
        return preferredGender.FirstOrDefault() ?? string.Empty;
    }

    public async Task<List<VolunteerAccount>> GetAvailableVolunteerAccounts(bool isAvailable, string city, long postalCode, int skillCategoryId,
        string preferredGender)
    {
        using var connection = _connectionFactory.GetConnection();
        var availableVolunteer = await connection.QueryAsync<VolunteerAccount>(TaskQueries.GetFreeVolunteers,
            new
            {
                @IsAvailable = isAvailable, @City = city, @PostalCode = postalCode, @SkillCategoryId = skillCategoryId,
                Gender = preferredGender
            });
        
        return availableVolunteer.ToList() ?? Enumerable.Empty<VolunteerAccount>().ToList();
    }

    public async Task<bool> AssignTaskToVolunteerAccount(int taskId, long elderlyUserId, DateTime assignedDate, long volunteerAccountId)
    {
        using var connection = _connectionFactory.GetConnection();
        var assignedTask = await connection.ExecuteScalarAsync<int>(TaskQueries.AssignTaskToVolunteers,
            new
            {
                TaskId = taskId, UserId = elderlyUserId, AssignedDate = assignedDate.Date,
                AssignedVolunteerId = volunteerAccountId
            });
        
        return assignedTask == 1;
    }

    public async Task<bool> UpdateTaskStatusAfterAssigning(bool assignmentStatus, int taskId)
    {
        using var connection = _connectionFactory.GetConnection();
        var update = await connection.ExecuteScalarAsync<int>(TaskQueries.UpdateTaskStatusAfterAssiging,
            new { @IsAssigned = assignmentStatus, @TaskId = taskId });
        return update == 1;
    }
}
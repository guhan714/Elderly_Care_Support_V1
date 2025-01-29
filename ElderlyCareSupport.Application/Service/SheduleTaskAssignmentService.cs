using ElderlyCareSupport.Application.IRepository;
using ElderlyCareSupport.Application.IService;
using Microsoft.Extensions.Logging;

namespace ElderlyCareSupport.Application.Service;

public class SheduleTaskAssignmentService : ITaskAssignmentService
{
    private readonly IAssignTaskRepository _assignTaskRepository;
    private Random random = new();
    private readonly ILogger<SheduleTaskAssignmentService> _logger;

    public SheduleTaskAssignmentService(IAssignTaskRepository assignTaskRepository,
        ILogger<SheduleTaskAssignmentService> logger)
    {
        _assignTaskRepository = assignTaskRepository;
        _logger = logger;
    }

    public async Task<bool> AssignTaskToVolunteer()
    {
        var success = false;
        var unAssignedTasks = await _assignTaskRepository.GetUnAssignedTasks();
        if (unAssignedTasks.Count == 0)
        {
            _logger.LogInformation("No unassigned tasks found");
            return false;
        }

        _logger.LogInformation("Assigning tasks to volunteer");
        foreach (var task in unAssignedTasks)
        {
            var preferredGender = await _assignTaskRepository.GetPreferredGender(task.PreferredGender);
            var availableVolunteers =
                await _assignTaskRepository.GetAvailableVolunteerAccounts(true, "", 0, task.TaskCategoryId,
                    preferredGender);

            if (availableVolunteers.Count == 0)
            {
                _logger.LogInformation("No volunteers free");
                continue;
            }

            var volunteerToAssign = availableVolunteers[random.Next(availableVolunteers.Count)];

            var assignedVolunteerAccount = await _assignTaskRepository.AssignTaskToVolunteerAccount(task.TaskId,
                task.ElderlyPersonId, DateTime.Now.Date, volunteerToAssign.Id);
            _logger.LogInformation("{Volunteer} has been assigned to the {Task}", volunteerToAssign.Id, task.TaskId);
            var updateTaskStatus =
                await _assignTaskRepository.UpdateTaskStatusAfterAssigning(assignedVolunteerAccount, task.TaskId);

            success = updateTaskStatus && assignedVolunteerAccount;

            _logger.LogInformation("Process has been completed");
        }

        return success;
    }
}
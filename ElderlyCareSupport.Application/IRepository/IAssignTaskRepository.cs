using System.Runtime.InteropServices.JavaScript;
using ElderlyCareSupport.Domain.Models;

namespace ElderlyCareSupport.Application.IRepository;

public interface IAssignTaskRepository
{
    Task<List<TaskDetails>> GetUnAssignedTasks();
    Task<string> GetPreferredGender(int genderId);
    Task<List<VolunteerAccount>>? GetAvailableVolunteerAccounts(bool isAvailable, string city, long postalCode, int skillCategoryId, string preferredGender);
    Task<bool> AssignTaskToVolunteerAccount(int taskId, long elderlyUserId, DateTime assignedDate, long volunteerAccountId);
    Task<bool> UpdateTaskStatusAfterAssigning(bool assignmentStatus, int taskId);
}
namespace ElderlyCareSupport.Application.IService;

public interface ITaskAssignmentService
{
    Task<bool> AssignTaskToVolunteer();
}
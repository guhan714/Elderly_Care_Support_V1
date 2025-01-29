using ElderlyCareSupport.Application.IService;
using ElderlyCareSupport.Application.Service;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace ElderlyCareSupport.Infrastructure.BackgroundServices;

[DisallowConcurrentExecution]
internal sealed class TaskAssigner : IJob
{
    private readonly ITaskAssignmentService _taskAssignmentService;

    public TaskAssigner(ITaskAssignmentService taskAssignmentService)
    {
        _taskAssignmentService = taskAssignmentService;
    }


    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            await _taskAssignmentService.AssignTaskToVolunteer();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
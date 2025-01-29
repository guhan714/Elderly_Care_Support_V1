using ElderlyCareSupport.Infrastructure.BackgroundServices;
using Microsoft.Extensions.Options;
using Quartz;

namespace ElderlyCareSupport.Infrastructure.Config;

internal class TaskAssignmentConfiguration : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        var job = JobKey.Create(nameof(TaskAssigner));
        options.AddJob<TaskAssigner>(builder => builder.WithIdentity(job))
            .AddTrigger(trigger =>
            {
                trigger.ForJob(job)
                    .WithSimpleSchedule(schedule => { schedule.WithIntervalInMinutes(10).RepeatForever(); });
            });
    }
}
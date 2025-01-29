using ElderlyCareSupport.Application.DTOs;
using ElderlyCareSupport.Application.IRepository;
using ElderlyCareSupport.Domain.Models;
using ElderlyCareSupport.Infrastructure.Config;
using ElderlyCareSupport.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace ElderlyCareSupport.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IFeeRepository, FeeRepository>();
        services.AddScoped<ILoginRepository, LoginRepository>();
        services.AddScoped<IRegistrationRepository, RegistrationRepository>();
        services.AddScoped<IForgotPasswordRepository, ForgotPasswordRepository>();
        services
            .AddScoped<IUserRepository<ElderCareAccount, ElderUserDto>,
                ElderlyUserRepository<ElderCareAccount, ElderUserDto>>();
        services
            .AddScoped<IUserRepository<VolunteerAccount, VolunteerUserDto>,
                VolunteerUserRepository<VolunteerAccount, VolunteerUserDto>>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ITaskMasterRepository, TaskCategoryRepository>();

        services.AddScoped<IAssignTaskRepository, TaskAssignmentRepository>();

        services.AddQuartz(options => { options.UseMicrosoftDependencyInjectionJobFactory(); });
        services.AddQuartzHostedService(options => { options.WaitForJobsToComplete = true; });
        services.ConfigureOptions<TaskAssignmentConfiguration>(); 
        return services;
    }
}
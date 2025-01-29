using ElderlyCareSupport.Application.Contracts.Requests;
using ElderlyCareSupport.Application.Contracts.Response;
using ElderlyCareSupport.Application.DTOs;
using ElderlyCareSupport.Application.Helpers;
using ElderlyCareSupport.Application.IService;
using ElderlyCareSupport.Application.Service;
using ElderlyCareSupport.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ElderlyCareSupport.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddScoped<EmptyModelProvider>();
        services.AddScoped<EmptyModelProvider>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IRegistrationService, RegistrationService>();
        services.AddScoped<IFeeService, FeeService>();
        services.AddScoped<IForgotPaswordService, ForgotPasswordService>();
        services.AddScoped<ITokenService, JwtTokenGenerator>();
        services.AddScoped<IUserProfileService<ElderUserDto>, ElderlyUserServices<ElderUserDto>>();
        services.AddScoped<IUserProfileService<VolunteerUserDto>, VolunteerUserService<VolunteerUserDto>>();
        services.AddScoped<IApiResponseFactoryService, ApiResponseFactory>();
        services.AddScoped<IModelValidatorService, ModelValidatorHelper>();
        services.AddScoped<IClock, ClockService>();
        services.AddScoped<IEmailService, EmailHelper>();
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<ITaskMaster, TaskCategoryService>();
        services.AddScoped<ITaskAssignmentService, SheduleTaskAssignmentService>();
        services.AddScoped<SheduleTaskAssignmentService>();
        return services;
    }
}
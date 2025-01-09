﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ElderlyCareSupport.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        return services;
    }
}
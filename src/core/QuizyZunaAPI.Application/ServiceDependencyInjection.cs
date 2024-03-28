using FluentValidation;

using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace QuizyZunaAPI.Application;

public static class ServiceDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ServiceDependencyInjection).Assembly;

        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(assembly)); 

        services.AddValidatorsFromAssembly(assembly);

        services.AddTransient(typeof(IPipelineBehavior<,>),typeof(RequestLoggingPipelineBehavior<,>));

        return services;
    }
}

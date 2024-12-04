using FluentValidation;

using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace QuizyZunaAPI.Application;

public static class ServiceDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        System.Reflection.Assembly assembly = typeof(ServiceDependencyInjection).Assembly;

        _ = services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(assembly));

        _ = services.AddValidatorsFromAssembly(assembly);

        _ = services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggingPipelineBehavior<,>));

        return services;
    }
}

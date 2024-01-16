using Microsoft.Extensions.DependencyInjection;

namespace QuizyZunaAPI.Application;

public static class ServiceDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ServiceDependencyInjection).Assembly;

        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(assembly)); 

        return services;
    }
}

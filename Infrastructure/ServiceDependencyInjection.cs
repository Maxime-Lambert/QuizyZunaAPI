using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services;
    }
}

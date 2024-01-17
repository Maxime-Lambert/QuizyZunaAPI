using Microsoft.Extensions.DependencyInjection;

namespace QuizyZunaAPI.Presentation;

public static class ServiceDependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        return services;
    }
}

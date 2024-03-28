using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using QuizyZunaAPI.Application;
using QuizyZunaAPI.Domain.Questions;
using QuizyZunaAPI.Persistence.Options;
using QuizyZunaAPI.Persistence.Repositories;

namespace QuizyZunaAPI.Persistence;

public static class ServiceDependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.ConfigureOptions<DatabaseOptionsSetup>();

        services.AddDbContext<ApplicationDbContext>((serviceProvider, dbContextOptionsBuilder) =>
        {
            var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;

            dbContextOptionsBuilder.UseNpgsql(databaseOptions.ConnectionString, npgsqlOptionsAction =>
            {
                npgsqlOptionsAction.CommandTimeout(databaseOptions.CommandTimeout);
                npgsqlOptionsAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
            });
            dbContextOptionsBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
            dbContextOptionsBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
            dbContextOptionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();

        return services;
    }
}

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
        _ = services.ConfigureOptions<DatabaseOptionsSetup>();

        _ = services.AddDbContext<ApplicationDbContext>((serviceProvider, dbContextOptionsBuilder) =>
        {
            DatabaseOptions databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;

            _ = dbContextOptionsBuilder.UseNpgsql(databaseOptions.ConnectionString, npgsqlOptionsAction =>
            {
                _ = npgsqlOptionsAction.CommandTimeout(databaseOptions.CommandTimeout);
                _ = npgsqlOptionsAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
            });
            _ = dbContextOptionsBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
            _ = dbContextOptionsBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
            _ = dbContextOptionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        _ = services.AddScoped<IUnitOfWork, UnitOfWork>();
        _ = services.AddScoped<IQuestionRepository, QuestionRepository>();

        return services;
    }
}

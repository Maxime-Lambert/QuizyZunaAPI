﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using QuizyZunaAPI.Application;
using QuizyZunaAPI.Persistence.Options;

namespace QuizyZunaAPI.Persistence;

public static class ServiceDependencyInjection
{
    private const string PersistenceAssemblyName = "QuizyZunaAPI.Persistence";

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
                npgsqlOptionsAction.MigrationsAssembly(PersistenceAssemblyName);
            });
            dbContextOptionsBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
            dbContextOptionsBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
        });

        services.AddScoped<IUnitOfWork, ApplicationDbContext>();

        return services;
    }
}

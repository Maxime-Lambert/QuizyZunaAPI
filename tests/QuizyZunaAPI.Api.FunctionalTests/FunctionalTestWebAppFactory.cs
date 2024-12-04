using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using QuizyZunaAPI.Persistence;

using Testcontainers.PostgreSql;

namespace QuizyZunaAPI.Api.FunctionalTests;

public sealed class FunctionalTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
#pragma warning disable CA2213 // Les champs supprimables doivent l'être
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder().WithImage("postgres:alpine")
                                                                                .WithDatabase("quizyzuna-test-db")
                                                                                .WithUsername("postgres")
                                                                                .WithPassword("postgres")
                                                                                .Build();

    public Task InitializeAsync()
    {
        return _dbContainer.StartAsync();
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        _ = builder.ConfigureTestServices(services =>
        {
            _ = services.RemoveAll<DbContextOptions<ApplicationDbContext>>();

            _ = services.AddDbContext<ApplicationDbContext>(options =>
            {
                _ = options.UseNpgsql(_dbContainer.GetConnectionString(), npgsqlOptionsAction =>
                {
                    _ = npgsqlOptionsAction.CommandTimeout(30);
                    _ = npgsqlOptionsAction.EnableRetryOnFailure(3);
                });
                _ = options.EnableDetailedErrors(false);
                _ = options.EnableSensitiveDataLogging(true);
                _ = options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        });
        base.ConfigureWebHost(builder);
    }
}
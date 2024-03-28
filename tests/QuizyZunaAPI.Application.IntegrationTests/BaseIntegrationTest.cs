using MediatR;

using Microsoft.Extensions.DependencyInjection;

using QuizyZunaAPI.Persistence;

namespace QuizyZunaAPI.Application.IntegrationTests;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IServiceScope _scope;
    protected ISender Sender { get; init; }
    protected ApplicationDbContext DbContext { get; init; }

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        ArgumentNullException.ThrowIfNull(factory);
        _scope = factory.Services.CreateScope();
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }
}

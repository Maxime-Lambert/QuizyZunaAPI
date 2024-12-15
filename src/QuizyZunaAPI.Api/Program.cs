using QuizyZunaAPI.Presentation;
using QuizyZunaAPI.Application;
using QuizyZunaAPI.Persistence;
using QuizyZunaAPI.Presentation.v1;

using Asp.Versioning;
using Asp.Versioning.ApiExplorer;

using Serilog;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Asp.Versioning.Builder;
using System.Security.Claims;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddPersistence()
                .AddApplication()
                .AddPresentation(builder.Configuration);


builder.Services.AddCors(options => options.AddPolicy("QuizyZuna React App", builder =>
    builder.WithOrigins("https://brave-coast-0cc72c303.5.azurestaticapps.net/")
            .AllowAnyHeader()
            .AllowAnyMethod()));

WebApplication app = builder.Build();

if (!app.Environment.IsProduction())
{
    using IServiceScope scope = app.Services.CreateScope();
    ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await dbContext.Database.MigrateAsync().ConfigureAwait(false);
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    IReadOnlyList<ApiVersionDescription> descriptions = app.DescribeApiVersions();

    foreach (var groupName in descriptions.Select(description => description.GroupName))
    {
        var url = $"/swagger/{groupName}/swagger.json";
        var name = groupName.ToUpperInvariant();
        options.SwaggerEndpoint(url, name);
    }
});

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseSerilogRequestLogging();

app.UseRateLimiter();

app.UseCors("QuizyZuna React App");

ApiVersionSet apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .ReportApiVersions()
    .Build();

RouteGroupBuilder versionedGroup = app.MapGroup("api/v{apiVersion:apiVersion}")
    .WithApiVersionSet(apiVersionSet);

versionedGroup.MapQuestionEndpoints();
versionedGroup.MapTopicsEndpoints();
versionedGroup.MapDifficultiesEndpoints();
versionedGroup.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

versionedGroup.MapGet("users/me", (ClaimsPrincipal claimsPrincipal) => claimsPrincipal.Claims.ToDictionary(c => c.Type, c => c.Value))
    .RequireAuthorization();

await app.RunAsync().ConfigureAwait(false);

public partial class Program
{
    protected Program() { }
}
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

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddPersistence()
                .AddApplication()
                .AddPresentation(builder);

builder.Services.AddCors(options =>
{
    options.AddPolicy("QuizyZuna React App", builder =>
    {
        builder.WithOrigins("*")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build(); 

if (!app.Environment.IsProduction())
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await db.Database.MigrateAsync().ConfigureAwait(false);
    }
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    IReadOnlyList<ApiVersionDescription> descriptions = app.DescribeApiVersions();

    foreach (var groupName in descriptions.Select(description => description.GroupName))
    {
        string url = $"/swagger/{groupName}/swagger.json";
        string name = groupName.ToUpperInvariant();
        options.SwaggerEndpoint(url, name);
    }
});

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseSerilogRequestLogging();

app.UseRateLimiter();

app.UseCors("QuizyZuna React App");

var apiVersionSet = app.NewApiVersionSet()
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

await app.RunAsync().ConfigureAwait(false);

public partial class Program 
{
    protected Program() { }
}
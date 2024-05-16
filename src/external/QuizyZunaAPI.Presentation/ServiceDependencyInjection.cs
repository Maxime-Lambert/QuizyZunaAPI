using System.Threading.RateLimiting;

using Asp.Versioning;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using QuizyZunaAPI.Api.Middlewares.ExceptionHandlers;

namespace QuizyZunaAPI.Presentation;

public static class ServiceDependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            options.AddPolicy("basic", httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 10,
                        Window = TimeSpan.FromSeconds(10)
                    }));
        });

        services.AddProblemDetails();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddHealthChecks()
            .AddNpgSql(builder?.Configuration.GetConnectionString("Database")!);

        services.AddExceptionHandler<QuestionNotFoundApplicationExceptionHandler>();
        services.AddExceptionHandler<QuestionsNotFoundWithFilersApplicationExceptionHandler>();
        services.AddExceptionHandler<WrongAnswersContainsCorrectAnswerDomainExceptionHandler>();
        services.AddExceptionHandler<WrongAnswersDoesNotContainThreeElementsDomainExceptionHandler>();
        services.AddExceptionHandler<GeneralExceptionHandler>();

        return services;
    }
}

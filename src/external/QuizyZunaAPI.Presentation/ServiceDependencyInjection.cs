using System.Threading.RateLimiting;

using Asp.Versioning;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using QuizyZunaAPI.Presentation.Middlewares.ExceptionHandlers;

namespace QuizyZunaAPI.Presentation;

public static class ServiceDependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, WebApplicationBuilder builder)
    {
        _ = services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            _ = options.AddPolicy("basic", httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 100,
                        Window = TimeSpan.FromSeconds(10)
                    }));
        });

        _ = services.AddProblemDetails();

        _ = services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        _ = services.AddHealthChecks()
            .AddNpgSql(builder?.Configuration.GetConnectionString("Database")!);

        _ = services.AddEndpointsApiExplorer();
        _ = services.AddSwaggerGen();


        _ = services.AddExceptionHandler<QuestionNotFoundApplicationExceptionHandler>();
        _ = services.AddExceptionHandler<QuestionsNotFoundWithFilersApplicationExceptionHandler>();
        _ = services.AddExceptionHandler<WrongAnswersContainsCorrectAnswerDomainExceptionHandler>();
        _ = services.AddExceptionHandler<WrongAnswersDoesNotContainThreeElementsDomainExceptionHandler>();
        _ = services.AddExceptionHandler<GeneralExceptionHandler>();

        return services;
    }
}

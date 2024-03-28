using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using QuizyZunaAPI.Domain.Questions.Exceptions;

namespace QuizyZunaAPI.Api.Middlewares.ExceptionHandlers;

public sealed class ThemesIsEmptyDomainExceptionHandler(ILogger<ThemesIsEmptyDomainExceptionHandler> logger) : IExceptionHandler
{
    private const string BadRequestTitle = "Bad Request";
    private const string ThemesIsEmptyDetail = "The Themes field can't be empty";
    private readonly ILogger<ThemesIsEmptyDomainExceptionHandler> _logger = logger;

    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ThemesIsEmptyDomainException)
        {
            return ValueTask.FromResult(false);
        }

        _logger.LogError(exception,
            "Exception occurred: {Message}",
            exception.Message);

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = BadRequestTitle,
            Detail = ThemesIsEmptyDetail
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return ValueTask.FromResult(true);
    }
}
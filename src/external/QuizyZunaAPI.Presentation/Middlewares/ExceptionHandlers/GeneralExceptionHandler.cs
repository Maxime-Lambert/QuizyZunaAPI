using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace QuizyZunaAPI.Api.Middlewares.ExceptionHandlers;

public sealed class GeneralExceptionHandler(ILogger<GeneralExceptionHandler> logger) : IExceptionHandler
{
    private const string InternalServerErrorTitle = "Internal Server Error";
    private const string GeneralExceptionDetail = "An unexpected error has occured";
    private readonly ILogger<GeneralExceptionHandler> _logger = logger;

    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception,
            "Exception occurred: {Message}",
            exception.Message);

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = InternalServerErrorTitle,
            Detail = GeneralExceptionDetail
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return ValueTask.FromResult(true);
    }
}

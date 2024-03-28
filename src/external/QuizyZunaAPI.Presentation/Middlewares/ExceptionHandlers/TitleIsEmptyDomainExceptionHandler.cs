using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using QuizyZunaAPI.Domain.Questions.Exceptions;

namespace QuizyZunaAPI.Api.Middlewares.ExceptionHandlers;

public sealed class TitleIsEmptyDomainExceptionHandler(ILogger<TitleIsEmptyDomainExceptionHandler> logger) : IExceptionHandler
{
    private const string BadRequestTitle = "Bad Request";
    private const string TitleIsEmptyDetail = "The Title field can't be empty";
    private readonly ILogger<TitleIsEmptyDomainExceptionHandler> _logger = logger;

    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not TitleIsEmptyDomainException)
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
            Detail = TitleIsEmptyDetail
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return ValueTask.FromResult(true);
    }
}
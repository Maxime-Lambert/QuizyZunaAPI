using QuizyZunaAPI.Domain.Questions.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace QuizyZunaAPI.Api.Middlewares.ExceptionHandlers;

public sealed class CorrectAnswerIsEmptyDomainExceptionHandler(ILogger<CorrectAnswerIsEmptyDomainExceptionHandler> logger) : IExceptionHandler
{
    private const string BadRequestTitle = "Bad Request";
    private const string CorrectAnswerIsEmptyDetail = "The CorrectAnswer field can't be empty";
    private readonly ILogger<CorrectAnswerIsEmptyDomainExceptionHandler> _logger = logger;

    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not CorrectAnswerIsEmptyDomainException)
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
            Detail = CorrectAnswerIsEmptyDetail
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return ValueTask.FromResult(true);
    }
}
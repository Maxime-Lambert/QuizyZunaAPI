using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using QuizyZunaAPI.Application.Questions.Exceptions;

namespace QuizyZunaAPI.Api.Middlewares.ExceptionHandlers;

public sealed class QuestionsNotFoundWithFilersApplicationExceptionHandler(ILogger<QuestionsNotFoundWithFilersApplicationExceptionHandler> logger) : IExceptionHandler
{
    private const string BadRequestTitle = "Bad Request";
    private const string QuestionsNotFoundWithFiltersDetail = "There are no questions with those filters";
    private readonly ILogger<QuestionsNotFoundWithFilersApplicationExceptionHandler> _logger = logger;

    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not QuestionsNotFoundWithFiltersApplicationException)
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
            Detail = QuestionsNotFoundWithFiltersDetail
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return ValueTask.FromResult(true);
    }
}
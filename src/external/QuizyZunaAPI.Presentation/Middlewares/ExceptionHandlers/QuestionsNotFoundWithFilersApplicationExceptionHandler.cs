using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using QuizyZunaAPI.Application.Questions.Exceptions;
using QuizyZunaAPI.Presentation;

namespace QuizyZunaAPI.Api.Middlewares.ExceptionHandlers;

public sealed class QuestionsNotFoundWithFilersApplicationExceptionHandler(ILogger<QuestionsNotFoundWithFilersApplicationExceptionHandler> logger) : IExceptionHandler
{
    private const string BadRequestTitle = "Bad Request";
    private const string QuestionsNotFoundWithFiltersDetail = "There are no questions with those filters";
    private readonly ILogger<QuestionsNotFoundWithFilersApplicationExceptionHandler> _logger = logger;

    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(httpContext);
        ArgumentNullException.ThrowIfNull(exception);

        if (exception is not QuestionsNotFoundWithFiltersApplicationException)
        {
            return ValueTask.FromResult(false);
        }

        _logger.LogException(exception.Message);

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
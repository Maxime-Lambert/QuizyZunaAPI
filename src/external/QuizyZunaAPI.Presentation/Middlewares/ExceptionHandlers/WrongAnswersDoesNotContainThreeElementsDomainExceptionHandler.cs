using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using QuizyZunaAPI.Domain.Questions.Exceptions;
using QuizyZunaAPI.Presentation;
using MediatR;

namespace QuizyZunaAPI.Api.Middlewares.ExceptionHandlers;

public sealed class WrongAnswersDoesNotContainThreeElementsDomainExceptionHandler(ILogger<WrongAnswersDoesNotContainThreeElementsDomainExceptionHandler> logger) : IExceptionHandler
{
    private const string BadRequestTitle = "Bad Request";
    private const string WrongAnswersDoesNotContainThreeElementsDetail = "The WrongAnswers field must contain exactly 3 elements";
    private readonly ILogger<WrongAnswersDoesNotContainThreeElementsDomainExceptionHandler> _logger = logger;

    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(httpContext);
        ArgumentNullException.ThrowIfNull(exception);

        if (exception is not WrongAnswersDoesNotContainThreeElementsDomainException)
        {
            return ValueTask.FromResult(false);
        }

        _logger.LogException(exception.Message);

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = BadRequestTitle,
            Detail = WrongAnswersDoesNotContainThreeElementsDetail
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return ValueTask.FromResult(true);
    }
}
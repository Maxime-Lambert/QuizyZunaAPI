using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using QuizyZunaAPI.Domain.Questions.Exceptions;
using QuizyZunaAPI.Presentation;

namespace QuizyZunaAPI.Api.Middlewares.ExceptionHandlers;

public sealed class WrongAnswersContainsCorrectAnswerDomainExceptionHandler(ILogger<WrongAnswersContainsCorrectAnswerDomainExceptionHandler> logger) : IExceptionHandler
{
    private const string BadRequestTitle = "Bad Request";
    private const string WrongAnswersContainsCorrectAnswerDetail = "The WrongAnswers field can't contain an element equal to the CorrectAnswer field";
    private readonly ILogger<WrongAnswersContainsCorrectAnswerDomainExceptionHandler> _logger = logger;

    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(httpContext);
        ArgumentNullException.ThrowIfNull(exception);

        if (exception is not WrongAnswersContainsCorrectAnswerDomainException)
        {
            return ValueTask.FromResult(false);
        }

        _logger.LogException(exception.Message);

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = BadRequestTitle,
            Detail = WrongAnswersContainsCorrectAnswerDetail
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return ValueTask.FromResult(true);
    }
}
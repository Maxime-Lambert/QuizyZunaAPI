﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using QuizyZunaAPI.Application.Questions.Exceptions;

namespace QuizyZunaAPI.Api.Middlewares.ExceptionHandlers;

public sealed class QuestionNotFoundApplicationExceptionHandler(ILogger<QuestionNotFoundApplicationExceptionHandler> logger) : IExceptionHandler
{
    private const string NotFoundTitle = "Not Found";
    private const string QuestionNotFoundDetail = "There is no question with this id";
    private readonly ILogger<QuestionNotFoundApplicationExceptionHandler> _logger = logger;

    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not QuestionNotFoundApplicationException)
        {
            return ValueTask.FromResult(false);
        }

        _logger.LogError(exception,
            "Exception occurred: {Message}",
            exception.Message);

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Title = NotFoundTitle,
            Detail = QuestionNotFoundDetail
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return ValueTask.FromResult(true);
    }
}
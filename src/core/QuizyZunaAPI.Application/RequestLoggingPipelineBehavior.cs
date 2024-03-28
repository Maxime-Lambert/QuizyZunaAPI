using MediatR;

using Microsoft.Extensions.Logging;

namespace QuizyZunaAPI.Application;

internal sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>(ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        logger.LogInformation("Processing Request {RequestName}", requestName);

        var response = await next();

        logger.LogInformation("Completed Request {RequestName}", requestName);

        return response;
    }
}

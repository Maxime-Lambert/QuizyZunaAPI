using MediatR;

using Microsoft.Extensions.Logging;

using QuizyZunaAPI.Presentation;

namespace QuizyZunaAPI.Application;

public sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>(ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(next);

        var requestName = typeof(TRequest).Name;

        logger.LogStartingRequest(requestName);

        var response = await next().ConfigureAwait(true);

        logger.LogFinishedRequest(requestName);

        return response;
    }
}

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using QuizyZunaAPI.Application.Questions.GetEnumerations.GetTopics;

namespace QuizyZunaAPI.Presentation.v1;

public static class TopicEndpoints
{
    private const string TopicsEndpointRouteValue = "questions_topics";

    public static IEndpointRouteBuilder MapTopicsEndpoints(this IEndpointRouteBuilder app)
    {
        var topicEndpoints = app.MapGroup(TopicsEndpointRouteValue);

        topicEndpoints.MapGet("", async (ISender sender) =>
        {
            var request = new GetAllTopicsQuery();

            var result = await sender.Send(request).ConfigureAwait(false);

            return Results.Ok(result);
        })
        .WithName("GetTopics")
        .MapToApiVersion(1)
        .RequireRateLimiting("basic");

        return app;
    }
}

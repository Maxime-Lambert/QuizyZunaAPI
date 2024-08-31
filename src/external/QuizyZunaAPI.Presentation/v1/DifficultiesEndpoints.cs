using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using QuizyZunaAPI.Application.Questions.GetEnumerations.GetDifficulties;

namespace QuizyZunaAPI.Presentation.v1;

public static class DifficultiesEndpoints
{
    private const string DifficultiesEndpointRouteValue = "questions_difficulties";

    public static IEndpointRouteBuilder MapDifficultiesEndpoints(this IEndpointRouteBuilder app)
    {
        var difficultyEndpoints = app.MapGroup(DifficultiesEndpointRouteValue);

        difficultyEndpoints.MapGet("", async (ISender sender) =>
        {
            var request = new GetAllDifficultiesQuery();

            var result = await sender.Send(request).ConfigureAwait(false);

            return Results.Ok(result);
        })
        .WithName("GetDifficulties")
        .MapToApiVersion(1)
        .RequireRateLimiting("basic");

        return app;
    }
}

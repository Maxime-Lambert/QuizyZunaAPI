using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using QuizyZunaAPI.Application.Questions.CreateQuestion;
using QuizyZunaAPI.Application.Questions.Delete;
using QuizyZunaAPI.Application.Questions.GetRange;
using QuizyZunaAPI.Application.Questions.GetById;
using QuizyZunaAPI.Application.Questions.Put;
using QuizyZunaAPI.Application.Questions.Adapters;

namespace QuizyZunaAPI.Presentation.v1;

public static class QuestionsEndpoints
{
    private const string QuestionsEndpointRouteValue = "questions";

    public static IEndpointRouteBuilder MapQuestionEndpoints(this IEndpointRouteBuilder app)
    {
        var questionEndpoints = app.MapGroup(QuestionsEndpointRouteValue);

        questionEndpoints.MapGet("", async (int? numberOfQuestions, string? difficulties, string? eras, string? themes,
            IValidator<GetAllQuestionsQuery> validator, ISender sender) =>
        {
            var request = new GetAllQuestionsQuery(numberOfQuestions, difficulties, eras, themes);

            var validationResult = await validator.ValidateAsync(request, default).ConfigureAwait(true);

            if (validationResult.IsValid)
            {
                var result = await sender.Send(request).ConfigureAwait(true);

                return Results.Ok(result);
            }

            return Results.BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
        })
        .WithName("GetQuestions")
        .MapToApiVersion(1)
        .RequireRateLimiting("basic");

        questionEndpoints.MapGet("/{id:guid}", async (Guid id, ISender sender) =>
        {
            var question = await sender.Send(new GetQuestionByIdQuery(id)).ConfigureAwait(true);

            return Results.Ok(question);
        })
        .WithName("GetQuestion")
        .MapToApiVersion(1)
        .RequireRateLimiting("basic");

        questionEndpoints.MapPost("", async (CreateQuestionRequest request, IValidator<CreateQuestionRequest> validator,
            ISender sender, HttpContext context) =>
        {
            var validationResult = await validator.ValidateAsync(request, default).ConfigureAwait(true);

            if (validationResult.IsValid)
            {
                var result = await sender.Send(request.ToCommand()).ConfigureAwait(true);

                return Results.CreatedAtRoute("GetQuestion", new { result.id }, result);
            }

            return Results.BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
        })
        .WithName("CreateQuestion")
        .MapToApiVersion(1)
        .RequireRateLimiting("basic");

        questionEndpoints.MapPut("/{id:guid}", async (Guid id, PutQuestionRequest request, IValidator<PutQuestionRequest> validator,
            ISender sender) =>
        {
            var validationResult = await validator.ValidateAsync(request, default).ConfigureAwait(true);

            if (validationResult.IsValid)
            {
                var result = await sender.Send(request.ToCommand(id)).ConfigureAwait(true);

                return Results.Ok(result);
            }

            return Results.BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
        })
        .WithName("PutQuestion")
        .MapToApiVersion(1)
        .RequireRateLimiting("basic");

        questionEndpoints.MapDelete("/{id:guid}", async (Guid id, ISender sender) =>
        {
            await sender.Send(new DeleteQuestionCommand(id)).ConfigureAwait(true);

            return Results.NoContent();
        })
        .WithName("DeleteQuestion")
        .MapToApiVersion(1)
        .RequireRateLimiting("basic");

        return app;
    }
}

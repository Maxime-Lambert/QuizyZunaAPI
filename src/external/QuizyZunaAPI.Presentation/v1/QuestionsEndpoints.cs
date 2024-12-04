using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using QuizyZunaAPI.Application.Questions.Delete;
using QuizyZunaAPI.Application.Questions.GetById;
using QuizyZunaAPI.Application.Questions.Put;
using QuizyZunaAPI.Application.Questions.Adapters;
using QuizyZunaAPI.Application.Questions.AddTimesAnswered;
using QuizyZunaAPI.Application.Questions.GetAll;
using QuizyZunaAPI.Application.Questions.Responses;
using QuizyZunaAPI.Application.Questions.Create;

namespace QuizyZunaAPI.Presentation.v1;

public static class QuestionsEndpoints
{
    private const string QuestionsEndpointRouteValue = "questions";

    public static IEndpointRouteBuilder MapQuestionEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder questionEndpoints = app.MapGroup(QuestionsEndpointRouteValue);

        _ = questionEndpoints.MapGet("", async (int? amount, string? difficulties, string? themes,
            bool? orderByAscendantDifficulty, bool? randomize, IValidator<GetAllQuestionsQuery> validator, ISender sender) =>
        {
            var request = new GetAllQuestionsQuery(amount, difficulties, themes, orderByAscendantDifficulty, randomize);

            FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(request, default).ConfigureAwait(true);

            if (validationResult.IsValid)
            {
                IEnumerable<QuestionWithoutIdResponse> result = await sender.Send(request).ConfigureAwait(true);

                return Results.Ok(result);
            }

            return Results.BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
        })
        .WithName("GetQuestions")
        .MapToApiVersion(1)
        .RequireRateLimiting("basic");

        _ = questionEndpoints.MapGet("/{id:guid}", async (Guid id, ISender sender) =>
        {
            QuestionResponse question = await sender.Send(new GetQuestionByIdQuery(id)).ConfigureAwait(true);

            return Results.Ok(question);
        })
        .WithName("GetQuestion")
        .MapToApiVersion(1)
        .RequireRateLimiting("basic");

        _ = questionEndpoints.MapPost("", async (CreateQuestionRequest request, IValidator<CreateQuestionRequest> validator,
            ISender sender, HttpContext context) =>
        {
            FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(request, default).ConfigureAwait(true);

            if (validationResult.IsValid)
            {
                QuestionResponse result = await sender.Send(request.ToCommand()).ConfigureAwait(true);

                return Results.CreatedAtRoute("GetQuestion", new { result.id }, result);
            }

            return Results.BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
        })
        .WithName("CreateQuestion")
        .MapToApiVersion(1)
        .RequireRateLimiting("basic");

        _ = questionEndpoints.MapPut("/{id:guid}", async (Guid id, PutQuestionRequest request, IValidator<PutQuestionRequest> validator,
            ISender sender) =>
        {
            FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(request, default).ConfigureAwait(true);

            if (validationResult.IsValid)
            {
                Domain.Questions.Question result = await sender.Send(request.ToCommand(id)).ConfigureAwait(true);

                return Results.Ok(result);
            }

            return Results.BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
        })
        .WithName("PutQuestion")
        .MapToApiVersion(1)
        .RequireRateLimiting("basic");

        _ = questionEndpoints.MapDelete("/{id:guid}", async (Guid id, ISender sender) =>
        {
            await sender.Send(new DeleteQuestionCommand(id)).ConfigureAwait(true);

            return Results.NoContent();
        })
        .WithName("DeleteQuestion")
        .MapToApiVersion(1)
        .RequireRateLimiting("basic");

        _ = questionEndpoints.MapPatch("/addTimesAnswered", async (AddTimesAnsweredCommand addTimesAnsweredCommand, ISender sender) =>
        {
            await sender.Send(addTimesAnsweredCommand).ConfigureAwait(true);

            return Results.NoContent();
        })
        .WithName("AddTimesAnswered")
        .MapToApiVersion(1)
        .RequireRateLimiting("basic");

        return app;
    }
}

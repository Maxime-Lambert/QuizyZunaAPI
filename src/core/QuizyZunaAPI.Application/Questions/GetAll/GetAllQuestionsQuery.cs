using MediatR;

using QuizyZunaAPI.Application.Questions.Responses;

namespace QuizyZunaAPI.Application.Questions.GetRange;

public sealed record GetAllQuestionsQuery(int? numberOfQuestions, string? difficulties, string? eras, string? themes) 
    : IRequest<IEnumerable<QuestionWithoutIdResponse>>;

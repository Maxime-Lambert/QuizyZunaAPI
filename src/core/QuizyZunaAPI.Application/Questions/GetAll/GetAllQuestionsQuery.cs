using MediatR;

using QuizyZunaAPI.Application.Questions.Responses;

namespace QuizyZunaAPI.Application.Questions.GetAll;

public sealed record GetAllQuestionsQuery(int? amount, string? difficulties, string? themes, bool? orderByAscendantDifficulty, bool? randomize)
    : IRequest<IEnumerable<QuestionWithoutIdResponse>>;

using MediatR;

namespace QuizyZunaAPI.Application.Questions.GetEnumerations.GetDifficulties;

public sealed record GetAllDifficultiesQuery() : IRequest<string[]>;

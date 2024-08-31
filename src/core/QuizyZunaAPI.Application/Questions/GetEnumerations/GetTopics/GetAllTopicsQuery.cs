using MediatR;

namespace QuizyZunaAPI.Application.Questions.GetEnumerations.GetTopics;

public sealed record GetAllTopicsQuery() : IRequest<string[]>;

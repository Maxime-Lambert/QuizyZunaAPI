using MediatR;

using QuizyZunaAPI.Application.Questions.GetEnumerations.GetTopics;
using QuizyZunaAPI.Domain.Questions.Enumerations;

namespace QuizyZunaAPI.Application.Questions.Put;

public sealed class GetAllTopicsQueryHandler() : IRequestHandler<GetAllTopicsQuery, string[]>
{
    public Task<string[]> Handle(GetAllTopicsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Enum.GetNames<Topic>());
    }
}

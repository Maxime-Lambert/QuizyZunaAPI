using MediatR;

using QuizyZunaAPI.Domain.Questions.Enumerations;

namespace QuizyZunaAPI.Application.Questions.GetEnumerations.GetTopics;

public sealed class GetAllTopicsQueryHandler() : IRequestHandler<GetAllTopicsQuery, string[]>
{
    public Task<string[]> Handle(GetAllTopicsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Enum.GetNames<Topic>());
    }
}

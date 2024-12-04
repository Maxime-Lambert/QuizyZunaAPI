using MediatR;

using QuizyZunaAPI.Domain.Questions.Enumerations;

namespace QuizyZunaAPI.Application.Questions.GetEnumerations.GetDifficulties;

public sealed class GetAllDifficultiesQueryHandler() : IRequestHandler<GetAllDifficultiesQuery, string[]>
{
    public Task<string[]> Handle(GetAllDifficultiesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Enum.GetNames<Difficulty>());
    }
}

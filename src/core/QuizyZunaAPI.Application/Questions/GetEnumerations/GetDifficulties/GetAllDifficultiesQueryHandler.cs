using MediatR;

using QuizyZunaAPI.Application.Questions.GetEnumerations.GetDifficulties;
using QuizyZunaAPI.Domain.Questions.Enumerations;

namespace QuizyZunaAPI.Application.Questions.Put;

public sealed class GetAllDifficultiesQueryHandler() : IRequestHandler<GetAllDifficultiesQuery, string[]>
{
    public Task<string[]> Handle(GetAllDifficultiesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Enum.GetNames<Difficulty>());
    }
}

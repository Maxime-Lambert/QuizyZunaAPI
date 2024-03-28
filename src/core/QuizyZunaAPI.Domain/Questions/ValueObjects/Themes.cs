using QuizyZunaAPI.Domain.Questions.Entities;
using QuizyZunaAPI.Domain.Questions.Exceptions;

namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record Themes
{
    public IEnumerable<Theme> Value { get; private init; }

    private Themes() { }

    public Themes(IEnumerable<Theme> themes)
    {
        if(!themes.Any())
        {
            throw new ThemesIsEmptyDomainException($"{nameof(themes)} can't be null");
        }

        Value = [.. themes];
    }
}
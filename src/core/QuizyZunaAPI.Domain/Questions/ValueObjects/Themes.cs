using QuizyZunaAPI.Domain.Questions.Enumerations;
using QuizyZunaAPI.Domain.Questions.Exceptions;

namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record Themes
{
    public IEnumerable<Theme> Value { get; private init; }

    private Themes() { }

    private Themes(IEnumerable<Theme> themes)
    {
        Value = [.. themes];
    }

    public static Themes Create(IEnumerable<Theme>? themes)
    {
        if (themes is null)
        {
            throw new TopicsIsNullDomainException($"{nameof(themes)} can't be null");
        }

        return new Themes(themes);
    }
}
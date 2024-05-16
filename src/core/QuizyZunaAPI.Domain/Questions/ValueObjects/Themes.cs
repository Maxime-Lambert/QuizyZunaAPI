using QuizyZunaAPI.Domain.Questions.Entities;
using QuizyZunaAPI.Domain.Questions.Exceptions;

namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record Themes
{
    public IEnumerable<Theme> Value { get; private init; } = new List<Theme>();

    private Themes() { }

    public Themes(IEnumerable<Theme> themes)
    {
        Value = [.. themes];
    }
}
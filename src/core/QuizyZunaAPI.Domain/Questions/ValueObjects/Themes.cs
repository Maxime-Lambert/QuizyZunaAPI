using QuizyZunaAPI.Domain.Questions.Entities;

namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record Themes
{
    public IEnumerable<Theme> Value { get; private set; } = [];

    public void Clear()
    {
        Value = [];
    }

    private Themes() { }

    public Themes(IEnumerable<Theme> themes)
    {
        Value = [.. themes];
    }
}
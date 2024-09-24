using QuizyZunaAPI.Domain.Questions.Entities;

namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record Themes
{
    public IEnumerable<Theme> Value { get; private set; } = new List<Theme>();

    public void Clear()
    {
        Value = new List<Theme>();
    }

    private Themes() { }

    public Themes(IEnumerable<Theme> themes)
    {
        Value = [.. themes];
    }
}
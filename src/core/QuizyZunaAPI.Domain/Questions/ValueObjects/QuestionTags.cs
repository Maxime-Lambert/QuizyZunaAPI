using QuizyZunaAPI.Domain.Questions.Enumerations;

namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record QuestionTags
{
    public Themes Themes { get; private init; } = null!;

    public Difficulty Difficulty { get; private init; }

    public QuestionYear Year { get; private init; } = null!;

    private QuestionTags() { }

    public QuestionTags(Themes themes, Difficulty difficulty, QuestionYear year)
    {
        ArgumentNullException.ThrowIfNull(nameof(themes));
        ArgumentNullException.ThrowIfNull(nameof(year));

        Themes = themes;
        Difficulty = difficulty;
        Year = year;
    }
}
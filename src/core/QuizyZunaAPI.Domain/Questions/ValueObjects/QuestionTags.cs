using QuizyZunaAPI.Domain.Questions.Enumerations;

namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record QuestionTags
{
    public Themes Themes { get; private init; } = null!;

    public Difficulty Difficulty { get; private init; }

    public Era Era { get; private init; }

    private QuestionTags() { }

    public QuestionTags(Themes themes, Difficulty difficulty, Era era)
    {
        ArgumentNullException.ThrowIfNull(nameof(themes));

        Themes = themes;
        Difficulty = difficulty;
        Era = era;
    }
}
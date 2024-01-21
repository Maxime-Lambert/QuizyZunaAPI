using QuizyZunaAPI.Domain.Questions.Enumerations;

namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record QuestionTags
{
    public Themes Themes { get; private init; }

    public Difficulty Difficulty { get; private init; }

    public Era Era { get; private init; }

    private QuestionTags() { }

    private QuestionTags(Themes themes, Difficulty difficulty, Era era)
    {
        Themes = themes;
        Difficulty = difficulty;
        Era = era;
    }

    public static QuestionTags Create(Themes themes, Difficulty difficulty, Era era = Enumerations.Era.None)
    {
        return new(themes, difficulty, era);
    }
}
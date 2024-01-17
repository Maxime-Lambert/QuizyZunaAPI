using QuizyZunaAPI.Domain.Questions.Enumerations;

namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record QuestionTags
{
    public Topics Topics { get; private init; }

    public Difficulty Difficulty { get; private init; }

    public Era Era { get; private init; }

    private QuestionTags(Topics topics, Difficulty difficulty, Era era)
    {
        Topics = topics;
        Difficulty = difficulty;
        Era = era;
    }

    public static QuestionTags Create(Topics topics, Difficulty difficulty, Era era = Era.None)
    {
        return new(topics, difficulty, era);
    }
}
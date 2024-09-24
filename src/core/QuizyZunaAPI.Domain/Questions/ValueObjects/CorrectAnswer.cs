namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record CorrectAnswer
{
    public string Value { get; private init; } = string.Empty;

    public TimesAnswered TimesAnswered { get; private init; } = null!;

    private CorrectAnswer() { }

    public CorrectAnswer(string correctAnswer, TimesAnswered timesAnswered)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(correctAnswer));

        Value = correctAnswer;
        TimesAnswered = timesAnswered;
    }
};
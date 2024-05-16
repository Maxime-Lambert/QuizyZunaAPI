using QuizyZunaAPI.Domain.Questions.Exceptions;

namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record CorrectAnswer
{
    public string Value { get; private init; } = string.Empty;

    private CorrectAnswer() { }

    public CorrectAnswer(string correctAnswer)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(correctAnswer));

        Value = correctAnswer;
    }
};
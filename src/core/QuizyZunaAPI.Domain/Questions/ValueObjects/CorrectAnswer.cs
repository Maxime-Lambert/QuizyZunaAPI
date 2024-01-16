using QuizyZunaAPI.Domain.Questions.Exceptions;

namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record CorrectAnswer
{
    public string Value { get; private init; }

    private CorrectAnswer(string correctAnswer)
    {
        Value = correctAnswer;
    }

    public static CorrectAnswer Create(string? correctAnswer)
    {
        if (string.IsNullOrEmpty(correctAnswer))
        {
            throw new CorrectAnswerIsNullDomainException($"{nameof(correctAnswer)} can't be null");
        }

        return new CorrectAnswer(correctAnswer);
    }
};
using QuizyZunaAPI.Domain.Questions.Exceptions;

namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record CorrectAnswer
{
    public string Value { get; private init; }

    private CorrectAnswer() { }

    public CorrectAnswer(string correctAnswer)
    {
        if(string.IsNullOrEmpty(correctAnswer))
        {
            throw new CorrectAnswerIsEmptyDomainException($"{nameof(correctAnswer)} can't be null");
        }

        Value = correctAnswer;
    }
};
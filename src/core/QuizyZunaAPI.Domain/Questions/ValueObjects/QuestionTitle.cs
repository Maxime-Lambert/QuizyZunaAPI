using QuizyZunaAPI.Domain.Questions.Exceptions;

namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record QuestionTitle
{
    public string Value { get; private init; } = string.Empty;

    private QuestionTitle() { }

    public QuestionTitle(string questionTitle)
    {
        ArgumentException.ThrowIfNullOrEmpty(questionTitle);

        Value = questionTitle;
    }
}

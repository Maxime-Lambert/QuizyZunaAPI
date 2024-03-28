using QuizyZunaAPI.Domain.Questions.Exceptions;

namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record QuestionTitle
{
    public string Value { get; private init; }

    private QuestionTitle() { }

    public QuestionTitle(string questionTitle)
    {
        if (string.IsNullOrEmpty(questionTitle))
        {
            throw new TitleIsEmptyDomainException($"{nameof(questionTitle)} can't be null");
        }

        Value = questionTitle;
    }
}

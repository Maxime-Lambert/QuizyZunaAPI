using QuizyZunaAPI.Domain.Questions.Exceptions;

namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record QuestionTitle
{
    public string Value { get; private init; }

    private QuestionTitle() { }

    private QuestionTitle(string questionTitle)
    {
        Value = questionTitle;
    }

    public static QuestionTitle Create(string? questionTitle)
    {
        if(string.IsNullOrEmpty(questionTitle))
        {
            throw new TitleIsNullDomainException($"{nameof(questionTitle)} can't be null");
        }

        return new QuestionTitle(questionTitle);
    }
}

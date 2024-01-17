using QuizyZunaAPI.Domain.Questions.Exceptions;

namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record WrongAnswersList
{
    public IReadOnlyCollection<string> Value { get; private init; }

    private WrongAnswersList(ICollection<string> value)
    {
        Value = [.. value];
    }

    public static WrongAnswersList Create(ICollection<string>? wrongAnswersList)
    {
        if (wrongAnswersList is null)
        {
            throw new WrongAnswersIsNullDomainException($"{nameof(wrongAnswersList)} can't be null");
        }

        if (wrongAnswersList.Count != 3)
        {
            throw new WrongAnswersDoesNotContainThreeElementsDomainException($"{nameof(wrongAnswersList)} must contain 3 elements");
        }

        return new WrongAnswersList(wrongAnswersList);
    }
}

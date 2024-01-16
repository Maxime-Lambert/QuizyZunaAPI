using System.Collections.ObjectModel;

using QuizyZunaAPI.Domain.Questions.Exceptions;

namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record WrongAnswers
{
    public IReadOnlyCollection<string> Value { get; private init; }

    private WrongAnswers(ICollection<string> value)
    {
        Value = [.. value];
    }

    public static WrongAnswers Create(ICollection<string>? wrongAnswers)
    {
        if (wrongAnswers is null)
        {
            throw new WrongAnswersIsNullDomainException($"{nameof(wrongAnswers)} can't be null");
        }

        if (wrongAnswers.Count != 3)
        {
            throw new WrongAnswersDoesNotContainThreeElementsDomainException($"{nameof(wrongAnswers)} must contain 3 elements");
        }

        return new WrongAnswers(wrongAnswers);
    }
}

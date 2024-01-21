using QuizyZunaAPI.Domain.Questions.Exceptions;

namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record WrongAnswers
{
    public IReadOnlyCollection<WrongAnswer> Value { get; private init; }

    private WrongAnswers() { }

    private WrongAnswers(ICollection<WrongAnswer> wrongAnswers)
    {
        Value = [.. wrongAnswers];
    }

    public static WrongAnswers Create(ICollection<WrongAnswer>? wrongAnswers)
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

    public bool ContainsCorrectAnswer(CorrectAnswer correctAnswer)
    {
        if(correctAnswer is null)
        {
            return false;
        }
        return Value!.Select(wrongAnswer => wrongAnswer.Value).Contains(correctAnswer.Value);
    }
}

using QuizyZunaAPI.Domain.Questions.Entities;
using QuizyZunaAPI.Domain.Questions.Exceptions;

namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record WrongAnswers
{
    private const int EXACT_NUMBER_OF_WRONGANSWERS = 3;

    public IReadOnlyCollection<WrongAnswer> Value { get; private init; }

    private WrongAnswers() { }

    public WrongAnswers(ICollection<WrongAnswer> wrongAnswers)
    {
        if(wrongAnswers.Count != EXACT_NUMBER_OF_WRONGANSWERS)
        {
            throw new WrongAnswersDoesNotContainThreeElementsDomainException($"{nameof(wrongAnswers)} must contain {EXACT_NUMBER_OF_WRONGANSWERS} elements");
        }

        Value = [.. wrongAnswers];
    }

    public void ThrowExceptionIfCorrectAnswerIsPresent(CorrectAnswer correctAnswer)
    {
        if(Value.Select(wrongAnswer => wrongAnswer.Value).Contains(correctAnswer.Value))
        {
            throw new WrongAnswersContainsCorrectAnswerDomainException($"{nameof(correctAnswer)} can't be contained by {nameof(WrongAnswers)}");
        } 
    }
}

using QuizyZunaAPI.Domain.Questions.Exceptions;

namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record WrongAnswersId
{
    public Guid Value { get; private init; }

    private WrongAnswersId(Guid wrongAnswersId)
    {
        Value = wrongAnswersId;
    }

    public static WrongAnswersId Create(Guid? wrongAnswersId)
    {
        if (wrongAnswersId is null)
        {
            throw new WrongAnswersIdIsNullDomainException($"{nameof(wrongAnswersId)} can't be null");
        }

        return new WrongAnswersId(wrongAnswersId.Value);
    }
}
using QuizyZunaAPI.Domain.Questions.Exceptions;

namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record QuestionId
{
    public Guid Value { get; private init; }

    private QuestionId(Guid questionId)
    {
        Value = questionId;
    }

    public static QuestionId Create(Guid? questionId)
    {
        if (questionId is null)
        {
            throw new QuestionIdIsNullDomainException($"{nameof(questionId)} can't be null");
        }

        return new QuestionId(questionId.Value);
    }
}

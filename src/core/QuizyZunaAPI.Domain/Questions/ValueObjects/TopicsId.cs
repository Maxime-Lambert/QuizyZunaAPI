using QuizyZunaAPI.Domain.Questions.Exceptions;

namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record TopicsId
{
    public Guid Value { get; private init; }

    private TopicsId(Guid topicsId)
    {
        Value = topicsId;
    }

    public static TopicsId Create(Guid? topicsId)
    {
        if (topicsId is null)
        {
            throw new TopicsIdIsNullDomainException($"{nameof(topicsId)} can't be null");
        }

        return new TopicsId(topicsId.Value);
    }
}
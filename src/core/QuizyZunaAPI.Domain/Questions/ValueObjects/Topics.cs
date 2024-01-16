using System.Collections.ObjectModel;

using QuizyZunaAPI.Domain.Questions.Enumerations;
using QuizyZunaAPI.Domain.Questions.Exceptions;

namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record Topics
{
    public IEnumerable<Topic> Value { get; private init; }

    private Topics(IEnumerable<Topic> topics)
    {
        Value = [.. topics];
    }

    public static Topics Create(IEnumerable<Topic>? topics)
    {
        if (topics is null)
        {
            throw new TopicsIsNullDomainException($"{nameof(topics)} can't be null");
        }

        return new Topics(topics);
    }
}
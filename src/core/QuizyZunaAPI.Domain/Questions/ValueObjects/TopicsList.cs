using QuizyZunaAPI.Domain.Questions.Enumerations;
using QuizyZunaAPI.Domain.Questions.Exceptions;

namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record TopicsList
{
    public IEnumerable<Topic> Value { get; private init; }

    private TopicsList(IEnumerable<Topic> topicsList)
    {
        Value = [.. topicsList];
    }

    public static TopicsList Create(IEnumerable<Topic>? topicsList)
    {
        if (topicsList is null)
        {
            throw new TopicsIsNullDomainException($"{nameof(topicsList)} can't be null");
        }

        return new TopicsList(topicsList);
    }
}
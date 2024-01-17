using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Domain.Questions;

public sealed class Topics
{
    public TopicsId Id { get; private init; }

    public QuestionId QuestionId { get; private init; }

    public TopicsList List { get; private init; }

    private Topics(TopicsId id, QuestionId questionId, TopicsList list)
    {
        Id = id;
        QuestionId = questionId;
        List = list;
    }

    public static Topics Create(TopicsId id, QuestionId questionId, TopicsList list)
    {
        return new(id, questionId, list);
    }
}

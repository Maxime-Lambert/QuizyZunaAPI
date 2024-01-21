using QuizyZunaAPI.Domain.Questions.Enumerations;
using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Domain.Questions;

public sealed class Theme
{
    public QuestionId QuestionId { get; private init; }

    public Topic Value { get; private init; }

    private Theme() { }

    private Theme(QuestionId questionId, Topic topic)
    {
        QuestionId = questionId;
        Value = topic;
    }

    public static Theme Create(QuestionId questionId, Topic topic)
    {
        return new Theme(questionId, topic);
    }
}

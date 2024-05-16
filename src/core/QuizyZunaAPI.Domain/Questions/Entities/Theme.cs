using QuizyZunaAPI.Domain.Questions.Enumerations;
using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Domain.Questions.Entities;

public sealed class Theme
{
    public QuestionId QuestionId { get; private init; } = null!;

    public Topic Value { get; private init; }

    private Theme() { }

    public static Theme Create(QuestionId questionId, Topic topic)
    {
        ArgumentNullException.ThrowIfNull(questionId);

        return new Theme
        {
            QuestionId = questionId,
            Value = topic
        };
    }
}
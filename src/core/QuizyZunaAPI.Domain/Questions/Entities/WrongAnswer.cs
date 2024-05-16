using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Domain.Questions.Entities;

public sealed class WrongAnswer
{
    public QuestionId QuestionId { get; private init; } = null!;

    public string Value { get; private init; } = string.Empty;

    private WrongAnswer() { }

    public static WrongAnswer Create(QuestionId questionId, string wrongAnswer)
    {
        ArgumentException.ThrowIfNullOrEmpty(wrongAnswer);
        ArgumentNullException.ThrowIfNull(questionId);

        return new WrongAnswer {
            QuestionId = questionId, 
            Value = wrongAnswer
        };
    }
}

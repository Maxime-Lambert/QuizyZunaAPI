using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Domain.Questions.Entities;

public sealed class WrongAnswer
{
    public QuestionId QuestionId { get; private init; } = null!;

    public TimesAnswered TimesAnswered { get; private init; } = null!;

    public string Value { get; private init; } = string.Empty;

    private WrongAnswer() { }

    public static WrongAnswer Create(QuestionId questionId, string wrongAnswer, TimesAnswered timesAnswered)
    {
        ArgumentException.ThrowIfNullOrEmpty(wrongAnswer);
        ArgumentNullException.ThrowIfNull(questionId);
        ArgumentNullException.ThrowIfNull(timesAnswered);

        return new WrongAnswer {
            QuestionId = questionId, 
            Value = wrongAnswer,
            TimesAnswered = timesAnswered
        };
    }
}

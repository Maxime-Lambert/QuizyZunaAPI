using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Domain.Questions.Entities;

public sealed class WrongAnswer
{
    public QuestionId QuestionId { get; private init; }

    public string Value { get; private init; }

    private WrongAnswer() { }

    public static WrongAnswer Create(QuestionId questionId, string wrongAnswer)
    {
        return new WrongAnswer {
            QuestionId = questionId, 
            Value = wrongAnswer
        };
    }
}

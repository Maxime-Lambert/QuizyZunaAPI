namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record QuestionId
{
    public Guid Value { get; private init; }

    private QuestionId() { }

    public QuestionId(Guid questionId)
    {
        Value = questionId;
    }
}

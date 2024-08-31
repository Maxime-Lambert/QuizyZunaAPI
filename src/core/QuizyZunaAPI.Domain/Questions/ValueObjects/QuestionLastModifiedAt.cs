namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record QuestionLastModifiedAt
{
    public DateTime Value { get; private init; }

    private QuestionLastModifiedAt() { }

    public QuestionLastModifiedAt(DateTime lastModifiedAt)
    {
        Value = lastModifiedAt;
    }
}

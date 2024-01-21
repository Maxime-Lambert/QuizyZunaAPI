namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed class WrongAnswer
{
    public QuestionId QuestionId { get; private init; }

    public string Value { get; private init; }

    private WrongAnswer() { }

    private WrongAnswer(QuestionId questionId, string wrongAnswer)
    {
        QuestionId = questionId;
        Value = wrongAnswer;
    }

    public static WrongAnswer Create(QuestionId questionId, string wrongAnswer)
    {
        return new(questionId, wrongAnswer);
    }
}

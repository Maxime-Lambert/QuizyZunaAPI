using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Domain.Questions;

public sealed class WrongAnswers
{
    public WrongAnswersId Id { get; private init; }

    public QuestionId QuestionId { get; private init; }

    public WrongAnswersList List { get; private init; }

    private WrongAnswers(WrongAnswersId id, QuestionId questionId, WrongAnswersList list)
    {
        Id = id;
        QuestionId = questionId;
        List = list;
    }

    public static WrongAnswers Create(WrongAnswersId id, QuestionId questionId, WrongAnswersList list)
    {
        return new(id, questionId, list);
    }
}

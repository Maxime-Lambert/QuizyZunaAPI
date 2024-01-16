using QuizyZunaAPI.Domain.Core;
using QuizyZunaAPI.Domain.Questions.Enumerations;
using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Domain.Questions;

public sealed class Question : IAmAggregateRoot
{
    public QuestionId QuestionId { get; private init; }

    public Title Text { get; private init; }

    public Answers Answers { get; private init; }

    public Topics Topics { get; private init; }

    public Difficulty Difficulty { get; private init; }

    public Era Era { get; private init; }

    private Question(QuestionId questionId, Title text, Answers answers, Topics topics, Difficulty difficulty, Era era)
    {
        QuestionId = questionId;
        Text = text;
        Difficulty = difficulty;
        Topics = topics;
        Answers = answers;
        Era = era;
    }

    public static Question Create(QuestionId questionId, Title text, Answers answers, Topics topics, Difficulty difficulty, Era era = Era.None)
    {
        return new Question(questionId, text, answers, topics, difficulty, era);
    }
}

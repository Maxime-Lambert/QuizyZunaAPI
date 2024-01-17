using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Domain.Questions;

public sealed class Question
{
    public QuestionId Id { get; private init; }

    public QuestionTitle Title { get; private init; }

    public Answers Answers { get; private init; }

    public QuestionTags Tags { get; private init; }

    private Question(QuestionId questionId, QuestionTitle title, Answers answers, QuestionTags tags)
    {
        Id = questionId;
        Title = title;
        Answers = answers;
        Tags = tags;
    }

    public static Question Create(QuestionId questionId, QuestionTitle title, Answers answers, QuestionTags tags)
    {
        return new Question(questionId, title, answers, tags);
    }
}

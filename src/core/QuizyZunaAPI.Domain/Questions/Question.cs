using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Domain.Questions;

public sealed class Question
{
    public QuestionId Id { get; set; }

    public QuestionTitle Title { get; set; }

    public Answers Answers { get; set; }

    public QuestionTags Tags { get; set; }

    private Question() { }

    public static Question Create(QuestionId id, QuestionTitle title, Answers answers, QuestionTags tags)
    {
        return new Question {
            Id = id,
            Title = title,
            Answers = answers,
            Tags = tags
        };
    }
}

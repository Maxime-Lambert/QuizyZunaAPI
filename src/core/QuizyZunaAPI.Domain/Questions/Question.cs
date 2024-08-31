using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Domain.Questions;

public sealed class Question
{
    public QuestionId Id { get; set; } = null!;

    public QuestionTitle Title { get; set; } = null!;

    public Answers Answers { get; set; } = null!;

    public QuestionTags Tags { get; set; } = null!;

    public QuestionLastModifiedAt LastModifiedAt { get; set; } = null!;

    private Question() { }

    public static Question Create(QuestionId id, QuestionTitle title, Answers answers, QuestionTags tags, QuestionLastModifiedAt lastModifiedAt)
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(title);
        ArgumentNullException.ThrowIfNull(answers);
        ArgumentNullException.ThrowIfNull(tags);
        ArgumentNullException.ThrowIfNull(lastModifiedAt);

        return new Question {
            Id = id,
            Title = title,
            Answers = answers,
            Tags = tags,
            LastModifiedAt = lastModifiedAt
        };
    }
}

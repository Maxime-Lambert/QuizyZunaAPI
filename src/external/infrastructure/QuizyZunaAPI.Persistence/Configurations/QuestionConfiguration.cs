using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizyZunaAPI.Domain.Questions;
using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Persistence.Configurations;

public sealed class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        _ = builder.HasKey(question => question.Id);

        _ = builder.Property(question => question.Id).HasConversion(
            questionId => questionId!.Value,
            value => new QuestionId(value));

        _ = builder.Property(question => question.Title).HasConversion(
            title => title!.Value,
            value => new QuestionTitle(value));

        _ = builder.Property(question => question.LastModifiedAt).HasConversion(
            lastModifiedAt => lastModifiedAt!.Value,
            value => new QuestionLastModifiedAt(value));

        _ = builder.OwnsOne(question => question.Answers, answersBuilder =>
        {
            _ = answersBuilder.OwnsOne(answers => answers.WrongAnswers, wrongAnswersBuilder => _ = wrongAnswersBuilder.OwnsMany(wrongAnswers => wrongAnswers.Value, wrongAnswerBuilder =>
                {
                    _ = wrongAnswerBuilder.Property(wrongAnswer => wrongAnswer.TimesAnswered).HasConversion(
                        timesAnswered => timesAnswered!.Value,
                        value => new TimesAnswered(value));
                    _ = wrongAnswerBuilder.WithOwner().HasForeignKey(wrongAnswer => wrongAnswer.QuestionId);
                }));

            _ = answersBuilder.OwnsOne(answers => answers.CorrectAnswer, correctAnswerBuilder => _ = correctAnswerBuilder.Property(correctAnswer => correctAnswer.TimesAnswered).HasConversion(
                    timesAnswered => timesAnswered!.Value,
                    value => new TimesAnswered(value)));
        });

        _ = builder.OwnsOne(question => question.Tags, tagsBuilder =>
        {
            _ = tagsBuilder.OwnsOne(tags => tags.Themes, themesBuilder => _ = themesBuilder.OwnsMany(themes => themes.Value)
                                        .WithOwner()
                                        .HasForeignKey(theme => theme.QuestionId));

            _ = tagsBuilder.Property(tags => tags.Difficulty);

            _ = tagsBuilder.Property(tags => tags.Year).HasConversion(
                            year => year!.Value,
                            value => new QuestionYear(value));
        });
    }
}

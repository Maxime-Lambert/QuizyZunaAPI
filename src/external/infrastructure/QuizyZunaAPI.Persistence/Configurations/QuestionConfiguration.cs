using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizyZunaAPI.Domain.Questions;
using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Persistence.Configurations;

internal sealed class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasKey(question => question.Id);

        builder.Property(question => question.Id).HasConversion(
            questionId => questionId!.Value,
            value => new QuestionId(value));

        builder.Property(question => question.Title).HasConversion(
            title => title!.Value,
            value => new QuestionTitle(value));

        builder.OwnsOne(question => question.Answers, answersBuilder =>
        {
            answersBuilder.OwnsOne(answers => answers.WrongAnswers, wrongAnswersBuilder =>
            {
                wrongAnswersBuilder.OwnsMany(wrongAnswers => wrongAnswers.Value)
                                        .WithOwner()
                                        .HasForeignKey(wrongAnswer => wrongAnswer.QuestionId);
            });
            answersBuilder.Property(answers => answers.CorrectAnswer).HasConversion(
                            correctAnswer => correctAnswer!.Value,
                            value => new CorrectAnswer(value));
        });

        builder.OwnsOne(question => question.Tags, tagsBuilder =>
        {
            tagsBuilder.OwnsOne(tags => tags.Themes, themesBuilder =>
            {
                themesBuilder.OwnsMany(themes => themes.Value)
                                        .WithOwner()
                                        .HasForeignKey(theme => theme.QuestionId);
            });

            tagsBuilder.Property(tags => tags.Difficulty);

            tagsBuilder.Property(tags => tags.Era);
        });
    }
}

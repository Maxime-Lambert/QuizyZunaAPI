﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizyZunaAPI.Domain.Questions;
using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Persistence.Configurations;

public sealed class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasKey(question => question.Id);

        builder.Property(question => question.Id).HasConversion(
            questionId => questionId!.Value,
            value => new QuestionId(value));

        builder.Property(question => question.Title).HasConversion(
            title => title!.Value,
            value => new QuestionTitle(value));

        builder.Property(question => question.LastModifiedAt).HasConversion(
            lastModifiedAt => lastModifiedAt!.Value,
            value => new QuestionLastModifiedAt(value));

        builder.OwnsOne(question => question.Answers, answersBuilder =>
        {
            answersBuilder.OwnsOne(answers => answers.WrongAnswers, wrongAnswersBuilder =>
            {
                wrongAnswersBuilder.OwnsMany(wrongAnswers => wrongAnswers.Value, wrongAnswerBuilder =>
                {
                    wrongAnswerBuilder.Property(wrongAnswer => wrongAnswer.TimesAnswered).HasConversion(
                        timesAnswered => timesAnswered!.Value,
                        value => new TimesAnswered(value));
                    wrongAnswerBuilder.WithOwner().HasForeignKey(wrongAnswer => wrongAnswer.QuestionId);
                });
            });

            answersBuilder.OwnsOne(answers => answers.CorrectAnswer, correctAnswerBuilder =>
            {
                correctAnswerBuilder.Property(correctAnswer => correctAnswer.TimesAnswered).HasConversion(
                    timesAnswered => timesAnswered!.Value,
                    value => new TimesAnswered(value));
            });
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

            tagsBuilder.Property(tags => tags.Year).HasConversion(
                            year => year!.Value,
                            value => new QuestionYear(value));
        });
    }
}

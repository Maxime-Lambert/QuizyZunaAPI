using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizyZunaAPI.Domain.Questions;
using QuizyZunaAPI.Domain.Questions.Enumerations;
using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Persistence.Configurations;

internal sealed class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasKey(question => question.Id);

        builder.Property(question => question.Id).HasConversion(
            questionId => questionId.Value,
            value => QuestionId.Create(value));

        builder.Property(question => question.Title).HasConversion(
            title => title.Value,
            value => QuestionTitle.Create(value));

        builder.HasOne<TopicsList>()
            .WithMany()
            .HasForeignKey(question => question.Id.Value)
            .IsRequired();
    }
}

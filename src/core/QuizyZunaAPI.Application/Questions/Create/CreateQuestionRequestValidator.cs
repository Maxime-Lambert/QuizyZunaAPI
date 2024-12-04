using FluentValidation;

using QuizyZunaAPI.Domain.Questions.Enumerations;
using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Application.Questions.Create;

public sealed class CreateQuestionRequestValidator : AbstractValidator<CreateQuestionRequest>
{
    public CreateQuestionRequestValidator()
    {
        _ = RuleFor(request => request.title).NotEmpty();
        _ = RuleFor(request => request.correctAnswer).NotEmpty();
        _ = RuleFor(request => request.wrongAnswers).NotEmpty();
        _ = RuleFor(request => request.difficulty).NotEmpty();
        _ = When(request => !string.IsNullOrEmpty(request.year), () =>
                RuleFor(request => request.year).Matches(QuestionYear.yearRegexValidation)
                .WithMessage(ValidationErrorMessages.YEAR_VALUE_INVALID));
        _ = When(request => request.difficulty is not null, () =>
                RuleFor(request => request.difficulty).Must(difficulty => Enum.IsDefined(typeof(Difficulty), difficulty))
                .WithMessage(ValidationErrorMessages.DIFFICULTY_VALUE_INVALID));
        _ = RuleFor(request => request.themes).NotEmpty();
        _ = When(request => request.themes is not null, () =>
                RuleFor(request => request.themes).Must(themes => themes.All(theme => Enum.IsDefined(typeof(Topic), theme)))
                .WithMessage(ValidationErrorMessages.THEMES_VALUE_INVALID));
    }
}

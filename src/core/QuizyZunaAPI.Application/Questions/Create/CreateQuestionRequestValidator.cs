using FluentValidation;

using QuizyZunaAPI.Application.Questions.CreateQuestion;
using QuizyZunaAPI.Domain.Questions.Enumerations;

namespace QuizyZunaAPI.Application.Questions.Create;

public sealed class CreateQuestionRequestValidator : AbstractValidator<CreateQuestionRequest>
{
    public CreateQuestionRequestValidator()
    {
        RuleFor(request => request.title).NotEmpty();
        RuleFor(request => request.correctAnswer).NotEmpty();
        RuleFor(request => request.wrongAnswers).NotEmpty();
        When(request => !string.IsNullOrEmpty(request.era), () =>
            RuleFor(request => request.era)
            .Must(era => Enum.IsDefined(typeof(Era), era))
            .WithMessage(ValidationErrorMessages.ERA_VALUE_INVALID));
        RuleFor(request => request.difficulty).NotEmpty();
        When(request => request.difficulty is not null, () =>
            RuleFor(request => request.difficulty).Must(difficulty => Enum.IsDefined(typeof(Difficulty), difficulty))
            .WithMessage(ValidationErrorMessages.DIFFICULTY_VALUE_INVALID));
        RuleFor(request => request.themes).NotEmpty();
        When(request => request.themes is not null, () =>
            RuleFor(request => request.themes).Must(themes => themes.All(theme => Enum.IsDefined(typeof(Topic), theme)))
            .WithMessage(ValidationErrorMessages.THEMES_VALUE_INVALID));
    }
}

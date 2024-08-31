using FluentValidation;

using QuizyZunaAPI.Domain.Questions.Enumerations;

namespace QuizyZunaAPI.Application.Questions.GetRange;

public sealed class GetAllQuestionsQueryValidator : AbstractValidator<GetAllQuestionsQuery>
{
    public GetAllQuestionsQueryValidator()
    {
        RuleFor(request => request.amount).InclusiveBetween(1, 40);
        When(request => request.themes is not null, () =>
            RuleFor(request => request.themes).Must(themes => Array.TrueForAll(themes!.Split(','), theme => Enum.IsDefined(typeof(Topic), theme)))
            .WithMessage(ValidationErrorMessages.THEMES_VALUE_INVALID));
        When(request => request.difficulties is not null, () =>
            RuleFor(request => request.difficulties).Must(difficulties => Array.TrueForAll(difficulties!.Split(','), difficulty => Enum.IsDefined(typeof(Difficulty), difficulty)))
            .WithMessage(ValidationErrorMessages.DIFFICULTIES_VALUE_INVALID));
    }
}

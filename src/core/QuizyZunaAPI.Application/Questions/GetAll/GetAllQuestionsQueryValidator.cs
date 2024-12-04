using FluentValidation;

using QuizyZunaAPI.Domain.Questions.Enumerations;

namespace QuizyZunaAPI.Application.Questions.GetAll;

public sealed class GetAllQuestionsQueryValidator : AbstractValidator<GetAllQuestionsQuery>
{
    public GetAllQuestionsQueryValidator()
    {
        _ = RuleFor(request => request.amount).InclusiveBetween(1, 40);
        _ = When(request => request.themes is not null, () =>
            RuleFor(request => request.themes).Must(themes => Array.TrueForAll(themes!.Split(','), theme => Enum.IsDefined(typeof(Topic), theme)))
            .WithMessage(ValidationErrorMessages.THEMES_VALUE_INVALID));
        _ = When(request => request.difficulties is not null, () =>
            RuleFor(request => request.difficulties).Must(difficulties => Array.TrueForAll(difficulties!.Split(','), difficulty => Enum.IsDefined(typeof(Difficulty), difficulty)))
            .WithMessage(ValidationErrorMessages.DIFFICULTIES_VALUE_INVALID));
    }
}

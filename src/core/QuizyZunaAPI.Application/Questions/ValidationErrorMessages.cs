using QuizyZunaAPI.Domain.Questions.Enumerations;
using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Application.Questions;

internal static class ValidationErrorMessages
{
    internal static readonly string ERA_VALUE_INVALID = $"{nameof(Era)} value must be one of those : {string.Join(", ", Enum.GetNames<Era>())}";
    internal static readonly string ERAS_VALUE_INVALID = $"{nameof(Era)} values must be in this list and separated by ',' : {string.Join(", ", Enum.GetNames<Era>())}";
    internal static readonly string DIFFICULTY_VALUE_INVALID = $"{nameof(Difficulty)} value must be one of those : {string.Join(", ", Enum.GetNames<Difficulty>())}";
    internal static readonly string DIFFICULTIES_VALUE_INVALID = $"{nameof(Difficulty)} values must be in this list and separated by ',' : {string.Join(", ", Enum.GetNames<Difficulty>())}";
    internal static readonly string THEMES_VALUE_INVALID = $"{nameof(Themes)} values must be in this list and separated by ',' : {string.Join(", ", Enum.GetNames<Topic>())}";
}

using System.Text.RegularExpressions;

using QuizyZunaAPI.Domain.Questions.Exceptions;

namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record QuestionYear
{
    public const string yearRegexValidation = @"[+|-]\d{11}";

    public string? Value { get; private init; }

    private QuestionYear() { }

    public QuestionYear(string? questionYear)
    {
        if (!string.IsNullOrEmpty(questionYear) && !Regex.IsMatch(questionYear, yearRegexValidation, RegexOptions.IgnoreCase))
        {
            throw new QuestionYearIsNotConformDomainException($"{nameof(questionYear)} must be in the format from -99999999999 to +99999999999 with exactly 11 decimals");
        }

        Value = questionYear;
    }
}

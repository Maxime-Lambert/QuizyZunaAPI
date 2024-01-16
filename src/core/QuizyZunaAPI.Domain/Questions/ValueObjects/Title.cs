using System.Runtime.CompilerServices;

using QuizyZunaAPI.Domain.Questions.Exceptions;

namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record Title
{
    public string Value { get; private init; }

    private Title(string title)
    {
        Value = title;
    }

    public static Title Create(string? title)
    {
        if(string.IsNullOrEmpty(title))
        {
            throw new TitleIsNullDomainException($"{nameof(title)} can't be null");
        }

        return new Title(title);
    }
}

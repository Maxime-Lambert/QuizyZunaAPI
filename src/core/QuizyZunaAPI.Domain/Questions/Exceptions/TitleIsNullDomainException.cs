using QuizyZunaAPI.Domain.Core;

namespace QuizyZunaAPI.Domain.Questions.Exceptions;

public sealed class TitleIsNullDomainException : DomainException
{
    public TitleIsNullDomainException(string message) : base(message)
    {
    }

    public TitleIsNullDomainException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public TitleIsNullDomainException()
    {
    }
}
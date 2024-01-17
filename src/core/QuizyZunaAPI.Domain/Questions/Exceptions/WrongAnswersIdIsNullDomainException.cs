using QuizyZunaAPI.Domain.Core;

namespace QuizyZunaAPI.Domain.Questions.Exceptions;

public sealed class WrongAnswersIdIsNullDomainException : DomainException
{
    public WrongAnswersIdIsNullDomainException(string message) : base(message)
    {
    }

    public WrongAnswersIdIsNullDomainException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public WrongAnswersIdIsNullDomainException()
    {
    }
}
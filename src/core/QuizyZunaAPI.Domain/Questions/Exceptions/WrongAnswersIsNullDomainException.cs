using QuizyZunaAPI.Domain.Core;

namespace QuizyZunaAPI.Domain.Questions.Exceptions;

public sealed class WrongAnswersIsNullDomainException : DomainException
{
    public WrongAnswersIsNullDomainException(string message) : base(message)
    {
    }

    public WrongAnswersIsNullDomainException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public WrongAnswersIsNullDomainException()
    {
    }
}

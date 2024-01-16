using QuizyZunaAPI.Domain.Core;

namespace QuizyZunaAPI.Domain.Questions.Exceptions;

public sealed class CorrectAnswerIsNullDomainException : DomainException
{
    public CorrectAnswerIsNullDomainException(string message) : base(message)
    {
    }

    public CorrectAnswerIsNullDomainException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public CorrectAnswerIsNullDomainException()
    {
    }
}

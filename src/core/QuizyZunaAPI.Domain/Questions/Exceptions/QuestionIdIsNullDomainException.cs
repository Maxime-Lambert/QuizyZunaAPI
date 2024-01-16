using QuizyZunaAPI.Domain.Core;

namespace QuizyZunaAPI.Domain.Questions.Exceptions;

public sealed class QuestionIdIsNullDomainException : DomainException
{
    public QuestionIdIsNullDomainException(string message) : base(message)
    {
    }

    public QuestionIdIsNullDomainException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public QuestionIdIsNullDomainException()
    {
    }
}

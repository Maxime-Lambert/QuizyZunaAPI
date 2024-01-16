using QuizyZunaAPI.Domain.Core;

namespace QuizyZunaAPI.Domain.Questions.Exceptions;

public sealed class TopicsIsNullDomainException : DomainException
{
    public TopicsIsNullDomainException(string message) : base(message)
    {
    }

    public TopicsIsNullDomainException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public TopicsIsNullDomainException()
    {
    }
}
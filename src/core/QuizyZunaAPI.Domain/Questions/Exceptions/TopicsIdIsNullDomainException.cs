using QuizyZunaAPI.Domain.Core;

namespace QuizyZunaAPI.Domain.Questions.Exceptions;

public sealed class TopicsIdIsNullDomainException : DomainException
{
    public TopicsIdIsNullDomainException(string message) : base(message)
    {
    }

    public TopicsIdIsNullDomainException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public TopicsIdIsNullDomainException()
    {
    }
}
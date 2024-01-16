using QuizyZunaAPI.Domain.Core;

namespace QuizyZunaAPI.Domain.Questions.Exceptions;

public sealed class WrongAnswersDoesNotContainThreeElementsDomainException : DomainException
{
    public WrongAnswersDoesNotContainThreeElementsDomainException(string message) : base(message)
    {
    }

    public WrongAnswersDoesNotContainThreeElementsDomainException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public WrongAnswersDoesNotContainThreeElementsDomainException()
    {
    }
}
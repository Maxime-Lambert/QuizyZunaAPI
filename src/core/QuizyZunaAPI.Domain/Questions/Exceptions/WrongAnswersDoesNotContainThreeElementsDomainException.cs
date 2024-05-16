namespace QuizyZunaAPI.Domain.Questions.Exceptions;

public sealed class WrongAnswersDoesNotContainThreeElementsDomainException : Exception
{

    public WrongAnswersDoesNotContainThreeElementsDomainException()
    {
    }

    public WrongAnswersDoesNotContainThreeElementsDomainException(string message) : base(message)
    {
    }

    public WrongAnswersDoesNotContainThreeElementsDomainException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
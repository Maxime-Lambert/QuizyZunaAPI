using QuizyZunaAPI.Domain.Core;

namespace QuizyZunaAPI.Domain.Questions.Exceptions;

public sealed class WrongAnswersContainsCorrectAnswerDomainException : DomainException
{
    public WrongAnswersContainsCorrectAnswerDomainException(string message) : base(message)
    {
    }

    public WrongAnswersContainsCorrectAnswerDomainException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public WrongAnswersContainsCorrectAnswerDomainException()
    {
    }
}
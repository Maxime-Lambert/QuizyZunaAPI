namespace QuizyZunaAPI.Domain.Questions.Exceptions;

public sealed class WrongAnswersContainsCorrectAnswerDomainException : Exception {

    public WrongAnswersContainsCorrectAnswerDomainException()
    {
    }

    public WrongAnswersContainsCorrectAnswerDomainException(string message) : base(message) 
    {
    }

    public WrongAnswersContainsCorrectAnswerDomainException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
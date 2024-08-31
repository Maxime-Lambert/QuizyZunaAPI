namespace QuizyZunaAPI.Domain.Questions.Exceptions;

public sealed class QuestionYearIsNotConformDomainException : Exception
{

    public QuestionYearIsNotConformDomainException()
    {
    }

    public QuestionYearIsNotConformDomainException(string message) : base(message)
    {
    }

    public QuestionYearIsNotConformDomainException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
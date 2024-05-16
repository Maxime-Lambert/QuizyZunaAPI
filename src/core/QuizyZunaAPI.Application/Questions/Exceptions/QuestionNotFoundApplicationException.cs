namespace QuizyZunaAPI.Application.Questions.Exceptions;

public sealed class QuestionNotFoundApplicationException : Exception
{

    public QuestionNotFoundApplicationException()
    {
    }

    public QuestionNotFoundApplicationException(string message) : base(message)
    {
    }

    public QuestionNotFoundApplicationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
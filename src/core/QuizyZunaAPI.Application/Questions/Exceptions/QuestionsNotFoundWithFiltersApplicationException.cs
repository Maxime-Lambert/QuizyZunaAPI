namespace QuizyZunaAPI.Application.Questions.Exceptions;

public sealed class QuestionsNotFoundWithFiltersApplicationException : Exception
{

    public QuestionsNotFoundWithFiltersApplicationException()
    {
    }

    public QuestionsNotFoundWithFiltersApplicationException(string message) : base(message)
    {
    }

    public QuestionsNotFoundWithFiltersApplicationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
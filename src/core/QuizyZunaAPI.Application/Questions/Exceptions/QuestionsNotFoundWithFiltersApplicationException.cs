namespace QuizyZunaAPI.Application.Questions.Exceptions;

public sealed class QuestionsNotFoundWithFiltersApplicationException(string message) : Exception(message) { }
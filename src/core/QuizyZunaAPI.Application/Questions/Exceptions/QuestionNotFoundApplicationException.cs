namespace QuizyZunaAPI.Application.Questions.Exceptions;

public sealed class QuestionNotFoundApplicationException(string message) : Exception(message) { }

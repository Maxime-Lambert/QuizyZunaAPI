namespace QuizyZunaAPI.Domain.Questions.Exceptions;

public sealed class CorrectAnswerIsEmptyDomainException(string message) : Exception(message) { }

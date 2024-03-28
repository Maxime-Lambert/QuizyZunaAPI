namespace QuizyZunaAPI.Domain.Questions.Exceptions;

public sealed class TitleIsEmptyDomainException(string message) : Exception(message) { }
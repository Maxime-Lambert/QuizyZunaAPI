namespace QuizyZunaAPI.Domain.Questions.Exceptions;

public sealed class ThemesIsEmptyDomainException(string message) : Exception(message) { }
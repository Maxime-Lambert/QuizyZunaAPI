namespace QuizyZunaAPI.Domain.Questions.Exceptions;

public sealed class WrongAnswersDoesNotContainThreeElementsDomainException(string message) : Exception(message) { }
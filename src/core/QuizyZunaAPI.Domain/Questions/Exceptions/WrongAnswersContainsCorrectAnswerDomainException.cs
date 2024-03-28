namespace QuizyZunaAPI.Domain.Questions.Exceptions;

public sealed class WrongAnswersContainsCorrectAnswerDomainException(string message) : Exception(message) { }
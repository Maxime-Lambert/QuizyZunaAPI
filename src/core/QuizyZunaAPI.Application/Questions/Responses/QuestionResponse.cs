namespace QuizyZunaAPI.Application.Questions.Responses;

public sealed record QuestionResponse(Guid id, string? title, string? correctAnswer, IEnumerable<string?> wrongAnswers,
    string? difficulty, string? era, IEnumerable<string?> themes);
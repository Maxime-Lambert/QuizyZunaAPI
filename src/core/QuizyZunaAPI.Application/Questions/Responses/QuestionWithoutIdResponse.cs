namespace QuizyZunaAPI.Application.Questions.Responses;

public sealed record QuestionWithoutIdResponse(string? title, string? correctAnswer, IEnumerable<string?> wrongAnswers,
    string? difficulty, string? year, IEnumerable<string?> themes, DateTime? lastModifiedAt);
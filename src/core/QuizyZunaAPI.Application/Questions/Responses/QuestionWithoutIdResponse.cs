namespace QuizyZunaAPI.Application.Questions.Responses;

public sealed record QuestionWithoutIdResponse(string? title, string? correctAnswer, IEnumerable<string?> wrongAnswers,
    string? difficulty, string? era, IEnumerable<string?> themes);
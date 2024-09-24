namespace QuizyZunaAPI.Application.Questions.Responses;

public sealed record QuestionWithoutIdResponse(string? title, string? correctAnswer, int? correctAnswerTimesAnswered,
    IEnumerable<string?> wrongAnswers, IEnumerable<int?> wrongAnswersTimesAnswered,
    string? difficulty, string? year, IEnumerable<string?> themes, DateTime? lastModifiedAt);
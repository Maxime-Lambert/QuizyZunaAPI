namespace QuizyZunaAPI.Application.Questions.Create;

public sealed record CreateQuestionRequest(string title, string correctAnswer, IEnumerable<string> wrongAnswers,
    string difficulty, string year, IEnumerable<string> themes);

namespace QuizyZunaAPI.Application.Questions.Put;

public sealed record PutQuestionRequest(string title, string correctAnswer, IEnumerable<string> wrongAnswers,
    string difficulty, string year, IEnumerable<string> themes);

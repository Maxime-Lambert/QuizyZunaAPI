namespace QuizyZunaAPI.Application.Questions.Put;

public sealed record PutQuestionRequest(string title, string correctAnswer, IEnumerable<string> wrongAnswers,
    string difficulty, string era, IEnumerable<string> themes);

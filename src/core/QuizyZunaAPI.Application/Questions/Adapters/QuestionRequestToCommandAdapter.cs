using QuizyZunaAPI.Domain.Questions.Entities;
using QuizyZunaAPI.Domain.Questions.ValueObjects;
using QuizyZunaAPI.Domain.Questions;
using QuizyZunaAPI.Domain.Questions.Enumerations;
using QuizyZunaAPI.Application.Questions.Create;
using QuizyZunaAPI.Application.Questions.Put;

namespace QuizyZunaAPI.Application.Questions.Adapters;

public static class QuestionRequestToCommandAdapter
{
    public static CreateQuestionCommand ToCommand(this CreateQuestionRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        QuestionId questionId = new(Guid.NewGuid());
        QuestionTitle title = new(request.title);
        CorrectAnswer correctAnswer = new(request.correctAnswer, new TimesAnswered(0));
        WrongAnswers wrongAnswers = new(request.wrongAnswers.Select(wrongAnswer => WrongAnswer.Create(questionId, wrongAnswer, new TimesAnswered(0))));
        Answers answers = new(correctAnswer, wrongAnswers);
        Themes themes = new(request.themes.Select(theme => Theme.Create(questionId, Enum.Parse<Topic>(theme))));
        QuestionYear year = new(request.year);
        QuestionLastModifiedAt questionLastModifiedAt = new(DateTime.UtcNow);
        QuestionTags tags = new(themes, Enum.Parse<Difficulty>(request.difficulty), year);

        return new CreateQuestionCommand(Question.Create(questionId, title, answers, tags, questionLastModifiedAt));
    }

    public static PutQuestionCommand ToCommand(this PutQuestionRequest request, Guid id)
    {
        ArgumentNullException.ThrowIfNull(request);

        QuestionId questionId = new(id);
        QuestionTitle title = new(request.title);
        CorrectAnswer correctAnswer = new(request.correctAnswer, new TimesAnswered(0));
        WrongAnswers wrongAnswers = new(request.wrongAnswers.Select(wrongAnswer => WrongAnswer.Create(questionId, wrongAnswer, new TimesAnswered(0))));
        Answers answers = new(correctAnswer, wrongAnswers);
        Themes themes = new(request.themes.Select(theme => Theme.Create(questionId, Enum.Parse<Topic>(theme))));
        QuestionYear year = new(request.year);
        QuestionLastModifiedAt questionLastModifiedAt = new(DateTime.UtcNow);
        QuestionTags tags = new(themes, Enum.Parse<Difficulty>(request.difficulty), year);

        return new PutQuestionCommand(Question.Create(questionId, title, answers, tags, questionLastModifiedAt));
    }
}

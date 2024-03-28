using System.Collections.ObjectModel;

using QuizyZunaAPI.Application.Questions.Put;
using QuizyZunaAPI.Application.Questions.CreateQuestion;
using QuizyZunaAPI.Domain.Questions.Entities;
using QuizyZunaAPI.Domain.Questions.ValueObjects;
using QuizyZunaAPI.Domain.Questions;
using QuizyZunaAPI.Domain.Questions.Enumerations;

namespace QuizyZunaAPI.Application.Questions.Adapters;

public static class QuestionRequestToCommandAdapter
{
    public static CreateQuestionCommand ToCommand(this CreateQuestionRequest request)
    {
        var questionId = new QuestionId(Guid.NewGuid());
        var title = new QuestionTitle(request.title);
        var correctAnswer = new CorrectAnswer(request.correctAnswer);
        Collection<WrongAnswer> wrongAnswersList = [];
        foreach (var wrongAnswer in request.wrongAnswers)
        {
            wrongAnswersList.Add(WrongAnswer.Create(questionId, wrongAnswer));
        }
        var wrongAnswers = new WrongAnswers(wrongAnswersList);
        var answers = new Answers(correctAnswer, wrongAnswers);
        Collection<Theme> themesList = [];
        foreach (var theme in request.themes)
        {
            themesList.Add(Theme.Create(questionId, Enum.Parse<Topic>(theme)));
        }
        var themes = new Themes(themesList);
        var era = Era.None;
        if (!string.IsNullOrEmpty(request.era))
        {
            era = Enum.Parse<Era>(request.era);
        }
        var tags = new QuestionTags(themes, Enum.Parse<Difficulty>(request.difficulty), era);

        return new CreateQuestionCommand(Question.Create(questionId, title, answers, tags));
    }

    public static PutQuestionCommand ToCommand(this PutQuestionRequest request, Guid id)
    {
        var questionId = new QuestionId(id);
        var title = new QuestionTitle(request.title);
        var correctAnswer = new CorrectAnswer(request.correctAnswer);
        Collection<WrongAnswer> wrongAnswersList = [];
        foreach (var wrongAnswer in request.wrongAnswers)
        {
            wrongAnswersList.Add(WrongAnswer.Create(questionId, wrongAnswer));
        }
        var wrongAnswers = new WrongAnswers(wrongAnswersList);
        var answers = new Answers(correctAnswer, wrongAnswers);
        Collection<Theme> themesList = [];
        foreach (var theme in request.themes)
        {
            themesList.Add(Theme.Create(questionId, Enum.Parse<Topic>(theme)));
        }
        var themes = new Themes(themesList);
        var era = Era.None;
        if (!string.IsNullOrEmpty(request.era))
        {
            era = Enum.Parse<Era>(request.era);
        }
        var tags = new QuestionTags(themes, Enum.Parse<Difficulty>(request.difficulty), era);

        return new PutQuestionCommand(Question.Create(questionId, title, answers, tags));
    }
}

using QuizyZunaAPI.Application.Questions.Responses;
using QuizyZunaAPI.Domain.Questions;

namespace QuizyZunaAPI.Application.Questions.Adapters;

internal static class QuestionToQuestionResponseAdapter
{
    internal static QuestionResponse ToResponse(this Question question)
    {
        return new QuestionResponse(
            question.Id.Value,
            question.Title.Value,
            question.Answers.CorrectAnswer.Value,
            question.Answers.WrongAnswers.Value.Select(wrongAnswer => wrongAnswer.Value),
            Enum.GetName(question.Tags.Difficulty),
            Enum.GetName(question.Tags.Era),
            question.Tags.Themes.Value.Select(theme => Enum.GetName(theme.Value)));
    }

    internal static QuestionWithoutIdResponse ToResponseWithoutId(this Question question)
    {
        return new QuestionWithoutIdResponse(
            question.Title.Value,
            question.Answers.CorrectAnswer.Value,
            question.Answers.WrongAnswers.Value.Select(wrongAnswer => wrongAnswer.Value),
            Enum.GetName(question.Tags.Difficulty),
            Enum.GetName(question.Tags.Era),
            question.Tags.Themes.Value.Select(theme => Enum.GetName(theme.Value)));
    }
}

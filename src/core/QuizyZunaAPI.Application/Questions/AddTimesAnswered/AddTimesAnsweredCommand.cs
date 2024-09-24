using MediatR;

namespace QuizyZunaAPI.Application.Questions.AddTimesAnswered;

public sealed record AddTimesAnsweredCommand(string QuestionTitle, string AnswerGiven) : IRequest;

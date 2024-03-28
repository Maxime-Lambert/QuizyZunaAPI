using MediatR;

using QuizyZunaAPI.Application.Questions.Responses;

namespace QuizyZunaAPI.Application.Questions.GetById;

public sealed record GetQuestionByIdQuery(Guid questionid) : IRequest<QuestionResponse>;
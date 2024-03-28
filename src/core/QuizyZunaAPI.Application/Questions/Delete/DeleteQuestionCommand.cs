using MediatR;

namespace QuizyZunaAPI.Application.Questions.Delete;

public sealed record DeleteQuestionCommand(Guid questionId) : IRequest;

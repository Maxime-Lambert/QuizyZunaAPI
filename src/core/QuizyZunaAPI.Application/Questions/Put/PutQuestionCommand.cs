using MediatR;

using QuizyZunaAPI.Domain.Questions;

namespace QuizyZunaAPI.Application.Questions.Put;

public sealed record PutQuestionCommand(Question question) : IRequest<Question>;

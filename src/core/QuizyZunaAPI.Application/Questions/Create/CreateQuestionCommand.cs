using MediatR;

using QuizyZunaAPI.Application.Questions.Responses;
using QuizyZunaAPI.Domain.Questions;

namespace QuizyZunaAPI.Application.Questions.CreateQuestion;

public sealed record CreateQuestionCommand(Question question) : IRequest<QuestionResponse>;
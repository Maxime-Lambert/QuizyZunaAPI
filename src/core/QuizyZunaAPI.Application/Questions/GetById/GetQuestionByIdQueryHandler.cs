using MediatR;

using QuizyZunaAPI.Application.Questions.Adapters;
using QuizyZunaAPI.Application.Questions.Exceptions;
using QuizyZunaAPI.Application.Questions.Responses;
using QuizyZunaAPI.Domain.Questions;
using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Application.Questions.GetById;

public sealed class GetQuestionByIdQueryHandler(IQuestionRepository questionRepository) : IRequestHandler<GetQuestionByIdQuery, QuestionResponse>
{
    private readonly IQuestionRepository _questionRepository = questionRepository;

    public async Task<QuestionResponse> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var question = await _questionRepository.GetByIdAsync(new QuestionId(request.questionid), cancellationToken).ConfigureAwait(true);

        if(question is null)
        {
            throw new QuestionNotFoundApplicationException($"A question with {request.questionid} can't be found");
        }

        return question.ToResponse();
    }
}

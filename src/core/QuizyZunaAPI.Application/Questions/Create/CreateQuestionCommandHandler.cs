using MediatR;

using QuizyZunaAPI.Application.Questions.Adapters;
using QuizyZunaAPI.Application.Questions.CreateQuestion;
using QuizyZunaAPI.Application.Questions.Responses;
using QuizyZunaAPI.Domain.Questions;

namespace QuizyZunaAPI.Application.Questions.Create;

public sealed class CreateQuestionCommandHandler(IUnitOfWork unitOfWork, IQuestionRepository questionRepository) 
    : IRequestHandler<CreateQuestionCommand, QuestionResponse>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IQuestionRepository _questionRepository = questionRepository;

    public async Task<QuestionResponse> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        await _questionRepository.AddAsync(request.question).ConfigureAwait(true);

        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(true);

        return request.question.ToResponse();
    }
}

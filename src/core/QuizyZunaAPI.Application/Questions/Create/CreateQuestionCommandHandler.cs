using MediatR;

using QuizyZunaAPI.Application.Questions.Adapters;
using QuizyZunaAPI.Application.Questions.CreateQuestion;
using QuizyZunaAPI.Application.Questions.Responses;
using QuizyZunaAPI.Domain.Questions;

namespace QuizyZunaAPI.Application.Questions.Create;

public sealed class CreateQuestionCommandHandler(IUnitOfWork caca, IQuestionRepository questionRepository) 
    : IRequestHandler<CreateQuestionCommand, QuestionResponse>
{
    private readonly IUnitOfWork _unitOfWork = caca;
    private readonly IQuestionRepository _questionRepository = questionRepository;

    public async Task<QuestionResponse> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        await _questionRepository.AddAsync(request.question);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return request.question.ToResponse();
    }
}

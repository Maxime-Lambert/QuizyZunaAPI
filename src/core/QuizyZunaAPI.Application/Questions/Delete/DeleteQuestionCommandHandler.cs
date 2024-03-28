using MediatR;

using QuizyZunaAPI.Domain.Questions;
using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Application.Questions.Delete;

public sealed class DeleteQuestionCommandHandler(IQuestionRepository questionRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteQuestionCommand>
{
    private readonly IQuestionRepository _questionRepository = questionRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetByIdAsync(new QuestionId(request.questionId), cancellationToken);
        
        if(question is not null)
        {
            _questionRepository.Delete(question);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}

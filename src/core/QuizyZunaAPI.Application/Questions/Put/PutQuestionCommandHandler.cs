using MediatR;

using QuizyZunaAPI.Application.Questions.Exceptions;
using QuizyZunaAPI.Domain.Questions;

namespace QuizyZunaAPI.Application.Questions.Put;

public sealed class PutQuestionCommandHandler(IQuestionRepository questionRepository, IUnitOfWork unitOfWork) : IRequestHandler<PutQuestionCommand, Question>
{
    private readonly IQuestionRepository _questionRepository = questionRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Question> Handle(PutQuestionCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var questionDoesNotExist = await _questionRepository.GetByIdAsync(request.question.Id, cancellationToken).ConfigureAwait(true) is null;

        if(questionDoesNotExist)
        {
            throw new QuestionNotFoundApplicationException($"A question with {request.question.Id.Value} can't be found");
        }

        _questionRepository.Update(request.question);

        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(true);

        return request.question;
    }
}

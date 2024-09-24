using MediatR;

using QuizyZunaAPI.Application.Questions.Exceptions;
using QuizyZunaAPI.Domain.Questions;
using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Application.Questions.AddTimesAnswered;

public sealed class AddTimesAnsweredCommandHandler(IUnitOfWork unitOfWork, IQuestionRepository questionRepository)
    : IRequestHandler<AddTimesAnsweredCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IQuestionRepository _questionRepository = questionRepository;

    public async Task Handle(AddTimesAnsweredCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        QuestionTitle questionTitle = new(request.QuestionTitle);

        var question = await _questionRepository.GetByTitleAsync(questionTitle, cancellationToken).ConfigureAwait(true);

        if(question is null)
        {
            throw new QuestionNotFoundApplicationException($"A question with {request.QuestionTitle} can't be found");
        }

        if(string.Equals(question.Answers.CorrectAnswer.Value,request.AnswerGiven,StringComparison.Ordinal))
        {
            question.Answers.CorrectAnswer.TimesAnswered.AddOne();
        } else
        {
            question.Answers.WrongAnswers.Value
                .FirstOrDefault(wrongAnswer => string.Equals(wrongAnswer.Value, request.AnswerGiven, StringComparison.Ordinal))!
                .TimesAnswered.AddOne();
        }

        _questionRepository.Update(question);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(true);
    }
}

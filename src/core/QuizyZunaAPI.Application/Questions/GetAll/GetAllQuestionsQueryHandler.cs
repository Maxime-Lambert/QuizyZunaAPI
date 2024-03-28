using MediatR;

using QuizyZunaAPI.Application.Questions.Adapters;
using QuizyZunaAPI.Application.Questions.Exceptions;
using QuizyZunaAPI.Application.Questions.Responses;
using QuizyZunaAPI.Domain.Questions;
using QuizyZunaAPI.Domain.Questions.Enumerations;

namespace QuizyZunaAPI.Application.Questions.GetRange;

public sealed class GetAllQuestionsQueryHandler(IQuestionRepository questionRepository) : 
    IRequestHandler<GetAllQuestionsQuery, IEnumerable<QuestionWithoutIdResponse>>
{
    private const int DEFAULT_NUMBER_OF_QUESTIONS_PER_QUERY = 40;
    private readonly IQuestionRepository _questionRepository = questionRepository;

    public async Task<IEnumerable<QuestionWithoutIdResponse>> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
    {
        var questions = await _questionRepository.GetAllAsync(cancellationToken);

        var numberOfQuestions = DEFAULT_NUMBER_OF_QUESTIONS_PER_QUERY;

        if(request.numberOfQuestions is not null)
        {
            numberOfQuestions = request.numberOfQuestions.Value;
        }

        IEnumerable<Difficulty> difficulties = Enum.GetValues(typeof(Difficulty)).Cast<Difficulty>();
        if(!string.IsNullOrEmpty(request.difficulties))
        {
            difficulties = request.difficulties.Split(',').Select(difficulty => (Difficulty)Enum.Parse(typeof(Difficulty), difficulty));
        }

        IEnumerable<Era> eras = Enum.GetValues(typeof(Era)).Cast<Era>();
        if (!string.IsNullOrEmpty(request.eras))
        {
            eras = request.eras.Split(',').Select(era => (Era)Enum.Parse(typeof(Era), era));
        }

        IEnumerable<Topic> themes = Enum.GetValues(typeof(Topic)).Cast<Topic>();
        if (!string.IsNullOrEmpty(request.themes))
        {
            themes = request.themes.Split(',').Select(theme => (Topic)Enum.Parse(typeof(Topic), theme));
        }

        var filteredQuestions = questions?.Where(question => 
                                    difficulties.Contains(question.Tags.Difficulty) &&
                                    eras.Contains(question.Tags.Era) &&
                                    themes.Intersect(question.Tags.Themes.Value.Select(theme => theme.Value)).Any())
                                        .Take(numberOfQuestions);

        if(filteredQuestions is null || !filteredQuestions.Any())
        {
            throw new QuestionsNotFoundWithFiltersApplicationException("No questions can be found with these filters");
        }

        return filteredQuestions.Select(question => question.ToResponseWithoutId());
    }
}

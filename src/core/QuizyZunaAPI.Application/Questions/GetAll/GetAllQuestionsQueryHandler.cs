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
        ArgumentNullException.ThrowIfNull(request);

        var questions = await _questionRepository.GetAllAsync(cancellationToken).ConfigureAwait(true);

        var numberOfQuestions = DEFAULT_NUMBER_OF_QUESTIONS_PER_QUERY;

        if(request.amount is not null)
        {
            numberOfQuestions = request.amount.Value;
        }

        IEnumerable<Difficulty> difficulties = Enum.GetValues(typeof(Difficulty)).Cast<Difficulty>();
        if(!string.IsNullOrEmpty(request.difficulties))
        {
            difficulties = request.difficulties.Split(',').Select(difficulty => (Difficulty)Enum.Parse(typeof(Difficulty), difficulty));
        }

        IEnumerable<Topic> themes = Enum.GetValues(typeof(Topic)).Cast<Topic>();
        if (!string.IsNullOrEmpty(request.themes))
        {
            themes = request.themes.Split(',').Select(theme => (Topic)Enum.Parse(typeof(Topic), theme));
        }

        var filteredQuestions = questions?.Where(question =>
                                    difficulties.Contains(question.Tags.Difficulty) &&
                                    themes.Intersect(question.Tags.Themes.Value.Select(theme => theme.Value)).Any());

        if(filteredQuestions is null || !filteredQuestions.Any())
        {
            throw new QuestionsNotFoundWithFiltersApplicationException("No questions can be found with these filters");
        }

        if (request.randomize.HasValue && request.randomize.Value)
        {
            filteredQuestions = filteredQuestions.OrderBy(_ => Guid.NewGuid());
        }

        filteredQuestions = filteredQuestions.Take(numberOfQuestions);

        if (request.orderByAscendantDifficulty.HasValue && request.orderByAscendantDifficulty.Value)
        {
            filteredQuestions = filteredQuestions.OrderBy(question => (int)question.Tags.Difficulty);
        }

        return filteredQuestions.Select(question => question.ToResponseWithoutId());
    }
}

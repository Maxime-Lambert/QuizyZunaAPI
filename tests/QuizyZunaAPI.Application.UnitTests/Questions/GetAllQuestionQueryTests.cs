using NSubstitute.ReturnsExtensions;

using QuizyZunaAPI.Application.Questions.Exceptions;
using QuizyZunaAPI.Application.Questions.GetById;
using QuizyZunaAPI.Application.Questions.GetRange;
using QuizyZunaAPI.Application.Questions.Responses;
using QuizyZunaAPI.Domain.Questions;
using QuizyZunaAPI.Domain.Questions.Entities;
using QuizyZunaAPI.Domain.Questions.Enumerations;
using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Application.UnitTests.Questions;

public class GetAllQuestionQueryTests
{
    private static readonly GetAllQuestionsQuery GetAllQuestionsQuery = new(4, "", "", "");

    private readonly GetAllQuestionsQueryHandler _handler;
    private readonly IQuestionRepository _questionRepositoryMock;

    public GetAllQuestionQueryTests()
    {
        _questionRepositoryMock = Substitute.For<IQuestionRepository>();
        _handler = new(_questionRepositoryMock);
    }

    [Fact]
    public async Task Handle_ShouldCallOnce_GetAllAsync_WhenQueryIsValid()
    {
        //Arrange
        var questionId = new QuestionId(Guid.NewGuid());
        var title = new QuestionTitle("Is this a question ?");
        var correctAnswer = new CorrectAnswer("Yes");
        ICollection<WrongAnswer> wrongAnswersList =
            [WrongAnswer.Create(questionId, "No"),
                WrongAnswer.Create(questionId, "Maybe"),
                WrongAnswer.Create(questionId, "Impossible")];
        var wrongAnswers = new WrongAnswers(wrongAnswersList);
        var answers = new Answers(correctAnswer, wrongAnswers);
        ICollection<Theme> themesList = [Theme.Create(questionId, Topic.Literature)];
        var themes = new Themes(themesList);
        var difficulty = Difficulty.Beginner;
        var era = Era.None;
        var questionTags = new QuestionTags(themes, difficulty, era);
        var question = Question.Create(questionId, title, answers, questionTags);

        _questionRepositoryMock.GetAllAsync(Arg.Any<CancellationToken>())
            .Returns([question]);

        //Act
        await _handler.Handle(GetAllQuestionsQuery, default);

        //Assert
        await _questionRepositoryMock.Received(1).GetAllAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldThrow_QuestionsNotFoundWithFilters_WhenQueryFindsNothing()
    {
        //Arrange
        _questionRepositoryMock.GetAllAsync(Arg.Any<CancellationToken>()).ReturnsNull();
        Task<IEnumerable<QuestionWithoutIdResponse>> Action() => _handler.Handle(GetAllQuestionsQuery, default);

        //Act
        var result = FluentActions.Awaiting(Action);

        //Assert
        await result.Should().ThrowExactlyAsync<QuestionsNotFoundWithFiltersApplicationException>()
            .WithMessage("No questions can be found with these filters");
    }
}

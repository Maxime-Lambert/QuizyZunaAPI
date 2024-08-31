using NSubstitute.ReturnsExtensions;

using QuizyZunaAPI.Application.Questions.Exceptions;
using QuizyZunaAPI.Application.Questions.GetRange;
using QuizyZunaAPI.Application.Questions.Responses;
using QuizyZunaAPI.Domain.Questions;
using QuizyZunaAPI.Domain.Questions.Entities;
using QuizyZunaAPI.Domain.Questions.Enumerations;
using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Application.UnitTests.Questions;

public class GetAllQuestionQueryTests
{
    private static readonly GetAllQuestionsQuery GetAllQuestionsQuery = new(4, "", "", null, null);

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
        QuestionId id = new(Guid.NewGuid());
        QuestionTitle title = new("Is this a question ?");
        CorrectAnswer correctAnswer = new("Yes");
        ICollection<WrongAnswer> wrongAnswersList =
            [WrongAnswer.Create(id, "No"),
                WrongAnswer.Create(id, "Maybe"),
                WrongAnswer.Create(id, "Impossible")];
        WrongAnswers wrongAnswers = new(wrongAnswersList);
        Answers answers = new(correctAnswer, wrongAnswers);
        ICollection<Theme> themesList = [Theme.Create(id, Topic.Literature)];
        Themes themes = new(themesList);
        var difficulty = Difficulty.Beginner;
        QuestionYear year = new("");
        var questionTags = new QuestionTags(themes, difficulty, year);
        QuestionLastModifiedAt lastModifiedAt = new(DateTime.UtcNow);
        var question = Question.Create(id, title, answers, questionTags, lastModifiedAt);

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

using NSubstitute.ReturnsExtensions;

using QuizyZunaAPI.Application.Questions.Exceptions;
using QuizyZunaAPI.Application.Questions.GetById;
using QuizyZunaAPI.Application.Questions.Responses;
using QuizyZunaAPI.Domain.Questions;
using QuizyZunaAPI.Domain.Questions.Entities;
using QuizyZunaAPI.Domain.Questions.Enumerations;
using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Application.UnitTests.Questions;

public class GetByIdQueryTests
{
    private static readonly GetQuestionByIdQuery GetQuestionByIdQuery = new(Guid.NewGuid());

    private readonly GetQuestionByIdQueryHandler _handler;
    private readonly IQuestionRepository _questionRepositoryMock;

    public GetByIdQueryTests()
    {
        _questionRepositoryMock = Substitute.For<IQuestionRepository>();
        _handler = new GetQuestionByIdQueryHandler(_questionRepositoryMock);
    }

    [Fact]
    public async Task Handle_ShouldCallOnce_GetByIdAsync_WhenQueryIsValid()
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

        _questionRepositoryMock.GetByIdAsync(Arg.Is<QuestionId>(questionId => questionId.Value == GetQuestionByIdQuery.questionid), Arg.Any<CancellationToken>())
            .Returns(question);

        //Act
        await _handler.Handle(GetQuestionByIdQuery, default);

        //Assert
        await _questionRepositoryMock.Received(1).GetByIdAsync(
            Arg.Is<QuestionId>(question => question.Value == GetQuestionByIdQuery.questionid),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldThrow_QuestionNotFound_WhenIdDoesntExist()
    {
        //Arrange
        _questionRepositoryMock.GetByIdAsync(
            Arg.Is<QuestionId>(questionId => questionId.Value == GetQuestionByIdQuery.questionid),
            Arg.Any<CancellationToken>()).ReturnsNull();
        Task<QuestionResponse> Action() => _handler.Handle(GetQuestionByIdQuery, default);

        //Act
        var result = FluentActions.Awaiting(Action);

        //Assert
        await result.Should().ThrowExactlyAsync<QuestionNotFoundApplicationException>()
            .WithMessage($"A question with {GetQuestionByIdQuery.questionid} can't be found");
    }
}

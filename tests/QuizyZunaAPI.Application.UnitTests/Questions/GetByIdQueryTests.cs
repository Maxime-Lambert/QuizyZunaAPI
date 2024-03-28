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

using NSubstitute.ReturnsExtensions;

using QuizyZunaAPI.Application.Questions.Adapters;
using QuizyZunaAPI.Application.Questions.Exceptions;
using QuizyZunaAPI.Application.Questions.GetById;
using QuizyZunaAPI.Application.Questions.Put;
using QuizyZunaAPI.Domain.Questions;
using QuizyZunaAPI.Domain.Questions.Entities;
using QuizyZunaAPI.Domain.Questions.Enumerations;
using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Application.UnitTests.Questions;

public class PutQuestionCommandTests
{
    private static readonly PutQuestionRequest PutQuestionRequest = new("Is this a Question ?", "Yes", ["No", "Maybe", "?"],
            "Novice", "", ["Literature", "Mangas"]);

    private readonly PutQuestionCommandHandler _handler;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IQuestionRepository _questionRepositoryMock;

    public PutQuestionCommandTests()
    {
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _questionRepositoryMock = Substitute.For<IQuestionRepository>();
        _handler = new PutQuestionCommandHandler(_questionRepositoryMock, _unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_ShouldCallOnce_GetByIdAsync_WhenCommandIsValid()
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

        var command = PutQuestionRequest.ToCommand(Guid.NewGuid());
        _questionRepositoryMock.GetByIdAsync(Arg.Is<QuestionId>(questionId => questionId == command.question.Id), Arg.Any<CancellationToken>())
            .Returns(question);

        //Act
        await _handler.Handle(command, default);

        //Assert
        await _questionRepositoryMock.Received(1).GetByIdAsync(Arg.Is<QuestionId>(questionId => questionId == command.question.Id), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldThrow_QuestionNotFound_WhenIdDoesntExist()
    {
        //Arrange
        var command = PutQuestionRequest.ToCommand(Guid.NewGuid());
        _questionRepositoryMock.GetByIdAsync(
            Arg.Is<QuestionId>(questionId => questionId == command.question.Id),
            Arg.Any<CancellationToken>()).ReturnsNull();
        Task<Question> Action() => _handler.Handle(command, default);

        //Act
        var result = FluentActions.Awaiting(Action);

        //Assert
        await result.Should().ThrowExactlyAsync<QuestionNotFoundApplicationException>()
            .WithMessage($"A question with {command.question.Id.Value} can't be found");
    }

    [Fact]
    public async Task Handle_ShouldCallOnce_Update_AndSaveChanges_WhenIdExists()
    {
        //Arrange
        QuestionId questionId = new(Guid.NewGuid());
        QuestionTitle title = new("Is this a question ?");
        CorrectAnswer correctAnswer = new("Yes");
        ICollection<WrongAnswer> wrongAnswersList =
            [WrongAnswer.Create(questionId, "No"),
                WrongAnswer.Create(questionId, "Maybe"),
                WrongAnswer.Create(questionId, "Impossible")];
        WrongAnswers wrongAnswers = new(wrongAnswersList);
        Answers answers = new(correctAnswer, wrongAnswers);
        ICollection<Theme> themesList = [Theme.Create(questionId, Topic.Literature)];
        Themes themes = new(themesList);
        var difficulty = Difficulty.Beginner;
        QuestionYear year = new("");
        var questionTags = new QuestionTags(themes, difficulty, year);
        QuestionLastModifiedAt lastModifiedAt = new(DateTime.UtcNow);
        var question = Question.Create(questionId, title, answers, questionTags, lastModifiedAt);

        var command = PutQuestionRequest.ToCommand(questionId.Value);
        _questionRepositoryMock.GetByIdAsync(Arg.Is<QuestionId>(questionId => questionId == command.question.Id), Arg.Any<CancellationToken>())
            .Returns(question);

        //Act
        await _handler.Handle(command, default);

        //Assert
        _questionRepositoryMock.Received(1).Update(Arg.Is<Question>(question => question == command.question));
        await _unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}

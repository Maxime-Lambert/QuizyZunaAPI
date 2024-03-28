using NSubstitute.ReturnsExtensions;

using QuizyZunaAPI.Application.Questions.Delete;
using QuizyZunaAPI.Domain.Questions;
using QuizyZunaAPI.Domain.Questions.Entities;
using QuizyZunaAPI.Domain.Questions.Enumerations;
using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Application.UnitTests.Questions;

public class DeleteQuestionCommandTests
{
    private static readonly DeleteQuestionCommand DeleteQuestionCommand = new(Guid.NewGuid());

    private readonly DeleteQuestionCommandHandler _handler;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IQuestionRepository _questionRepositoryMock;

    public DeleteQuestionCommandTests()
    {
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _questionRepositoryMock = Substitute.For<IQuestionRepository>();
        _handler = new DeleteQuestionCommandHandler(_questionRepositoryMock, _unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_ShouldCallOnce_GetByIdAsync_WhenCommandIsValid()
    {
        //Arrange

        //Act
        await _handler.Handle(DeleteQuestionCommand, default);

        //Assert
        await _questionRepositoryMock.Received(1).GetByIdAsync(
            Arg.Is<QuestionId>(questionId => questionId.Value == DeleteQuestionCommand.questionId),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldCallOnce_Delete_AndSaveChanges_WhenQuestionExists()
    {
        //Arrange
        var id = new QuestionId(Guid.NewGuid());
        var title = new QuestionTitle("Is this a question ?");
        var correctAnswer = new CorrectAnswer("Yes");
        ICollection<WrongAnswer> wrongAnswersList =
            [WrongAnswer.Create(id, "No"),
                WrongAnswer.Create(id, "Maybe"),
                WrongAnswer.Create(id, "Impossible")];
        var wrongAnswers = new WrongAnswers(wrongAnswersList);
        var answers = new Answers(correctAnswer, wrongAnswers);
        ICollection<Theme> themesList = [Theme.Create(id, Topic.Literature)];
        var themes = new Themes(themesList);
        var difficulty = Difficulty.Beginner;
        var era = Era.None;
        var questionTags = new QuestionTags(themes, difficulty, era);
        var question = Question.Create(id, title, answers, questionTags);

        _questionRepositoryMock.GetByIdAsync(Arg.Is<QuestionId>(questionId => questionId == id), Arg.Any<CancellationToken>())
            .Returns(question);

        var command = DeleteQuestionCommand with { questionId = id.Value };

        //Act
        await _handler.Handle(command, default);

        //Assert
        _questionRepositoryMock.Received(1).Delete(Arg.Is<Question>(question => question.Id == id));
        await _unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldNotCall_Delete_AndSaveChanges_WhenQuestionDoesNotExist()
    {
        //Arrange
        _questionRepositoryMock.GetByIdAsync(
            Arg.Is<QuestionId>(questionId => questionId.Value == DeleteQuestionCommand.questionId),
            Arg.Any<CancellationToken>()).ReturnsNull();

        //Act
        await _handler.Handle(DeleteQuestionCommand, default);

        //Assert
        _questionRepositoryMock.DidNotReceive().Delete(Arg.Is<Question>(question => question.Id.Value == DeleteQuestionCommand.questionId));
        await _unitOfWorkMock.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}

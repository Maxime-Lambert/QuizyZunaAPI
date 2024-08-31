using QuizyZunaAPI.Application.Questions.Adapters;
using QuizyZunaAPI.Application.Questions.Create;
using QuizyZunaAPI.Application.Questions.CreateQuestion;
using QuizyZunaAPI.Domain.Questions;

namespace QuizyZunaAPI.Application.UnitTests.Questions;

public class CreateQuestionCommandTests
{
    private static readonly CreateQuestionRequest CreateQuestionRequest = new("Is this a Question ?", "Yes", ["No", "Maybe", "?"],
            "Novice", "", ["Literature", "Mangas"]);

    private readonly CreateQuestionCommandHandler _handler;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IQuestionRepository _questionRepositoryMock;

    public CreateQuestionCommandTests()
    {
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _questionRepositoryMock = Substitute.For<IQuestionRepository>();
        _handler = new CreateQuestionCommandHandler(_unitOfWorkMock, _questionRepositoryMock);
    }

    [Fact]
    public async Task Handle_ShouldCallOnce_AddAsync_AndSaveChanges_WhenCommandIsValid()
    {
        //Arrange
        var command = CreateQuestionRequest.ToCommand();

        //Act
        await _handler.Handle(command, default);

        //Assert
        await _questionRepositoryMock.Received(1).AddAsync(Arg.Is<Question>(question => question.Id == command.question.Id));
        await _unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}
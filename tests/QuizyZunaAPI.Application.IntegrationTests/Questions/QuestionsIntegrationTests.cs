using QuizyZunaAPI.Application.Questions.Adapters;
using QuizyZunaAPI.Application.Questions.CreateQuestion;
using QuizyZunaAPI.Application.Questions.Delete;
using QuizyZunaAPI.Application.Questions.GetById;
using QuizyZunaAPI.Application.Questions.GetRange;
using QuizyZunaAPI.Application.Questions.Put;

namespace QuizyZunaAPI.Application.IntegrationTests.Questions;

public class QuestionsIntegrationTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private static readonly CreateQuestionRequest CreateQuestionRequest = new("Is this a Question ?", "Yes", ["No", "Maybe", "?"],
            "Novice", "", ["Literature", "Mangas"]);

    [Fact]
    public async Task Create_ShouldAdd_NewQuestionToDatabase_WhenCommandIsValid()
    {
        //Arrange
        var command = CreateQuestionRequest.ToCommand();

        //Act
        await Sender.Send(command);
        var questionInDb = DbContext.Questions.FirstOrDefault(question => question.Id == command.question.Id);

        //Assert
        questionInDb.Should().NotBeNull();
    }

    [Fact]
    public async Task Put_ShouldUpdate_QuestionTitle_WhenCommandIsValid()
    {
        //Arrange
        var command = CreateQuestionRequest.ToCommand();
        await Sender.Send(command);
        DbContext.ChangeTracker.Clear();
        var putRequest = new PutQuestionRequest("Did this question change title ?", "Yes", ["No", "Maybe", "?"],
            "Novice", "", ["Literature", "Mangas"]);
        var putCommand = putRequest.ToCommand(command.question.Id.Value);

        //Act
        await Sender.Send(putCommand);
        var questionInDb = DbContext.Questions.FirstOrDefault(question => question.Id == command.question.Id);

        //Assert
        questionInDb!.Title.Value.Should().Be(putRequest.title);
    }

    [Fact]
    public async Task Delete_ShouldDelete_Question_WhenCommandIsValid()
    {
        //Arrange
        var command = CreateQuestionRequest.ToCommand();
        await Sender.Send(command);
        DbContext.ChangeTracker.Clear();
        var deleteCommand = new DeleteQuestionCommand(command.question.Id.Value);

        //Act
        await Sender.Send(deleteCommand);
        var questionInDb = DbContext.Questions.FirstOrDefault(question => question.Id == command.question.Id);

        //Assert
        questionInDb.Should().BeNull();
    }

    [Fact]
    public async Task Get_ShouldReturn_Question_WhenCommandIsValid()
    {
        //Arrange
        var command = CreateQuestionRequest.ToCommand();
        await Sender.Send(command);
        DbContext.ChangeTracker.Clear();
        var getCommand = new GetQuestionByIdQuery(command.question.Id.Value);

        //Act
        var result = await Sender.Send(getCommand);

        //Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetAll_ShouldReturn_ThreeQuestions_WhenCommandIsValid()
    {
        //Arrange
        var request = CreateQuestionRequest with { themes = ["Gastronomy", "LivingBeings"] };
        var command = request.ToCommand();
        await Sender.Send(command);
        DbContext.ChangeTracker.Clear();
        request = CreateQuestionRequest with { themes = ["Gastronomy"] };
        command = request.ToCommand();
        await Sender.Send(command);
        DbContext.ChangeTracker.Clear();
        request = CreateQuestionRequest with { themes = ["LivingBeings"] };
        command = request.ToCommand();
        await Sender.Send(command);
        DbContext.ChangeTracker.Clear();

        request = CreateQuestionRequest with { themes = ["VideoGames"] };
        command = request.ToCommand();
        await Sender.Send(command);
        DbContext.ChangeTracker.Clear();
        request = CreateQuestionRequest with { themes = ["History"] };
        command = request.ToCommand();
        await Sender.Send(command);
        DbContext.ChangeTracker.Clear();
        request = CreateQuestionRequest with { themes = ["History", "VideoGames"] };
        command = request.ToCommand();
        await Sender.Send(command);
        DbContext.ChangeTracker.Clear();
        request = CreateQuestionRequest with { themes = ["Architecture"] };
        command = request.ToCommand();
        await Sender.Send(command);
        DbContext.ChangeTracker.Clear();
        request = CreateQuestionRequest with { themes = ["Gastronomy"], difficulty = "Difficult" };
        command = request.ToCommand();
        await Sender.Send(command);
        DbContext.ChangeTracker.Clear();
        request = CreateQuestionRequest with { themes = ["Gastronomy"], difficulty = "Beginner" };
        command = request.ToCommand();
        await Sender.Send(command);
        DbContext.ChangeTracker.Clear();
        var getAllRequest = new GetAllQuestionsQuery(3, "Novice", "Gastronomy,LivingBeings", null, null);

        //Act
        var result = await Sender.Send(getAllRequest);

        //Assert
        result.Count().Should().Be(3);
        result.Should().OnlyContain(question => string.Equals(question.difficulty, "Novice", StringComparison.Ordinal) &&
                                               question.themes.All(theme => string.Equals(theme, "Gastronomy", StringComparison.Ordinal) 
                                                                    || string.Equals(theme, "LivingBeings", StringComparison.Ordinal)));
    }
}

using System.Net;
using System.Net.Http.Json;

using QuizyZunaAPI.Application.Questions.CreateQuestion;
using QuizyZunaAPI.Application.Questions.Put;
using QuizyZunaAPI.Application.Questions.Responses;

namespace QuizyZunaAPI.Api.FunctionalTests.Questions;

public class PutQuestionTests(FunctionalTestWebAppFactory functionalTestWebAppFactory) : BaseFunctionalTest(functionalTestWebAppFactory)
{
    private static readonly PutQuestionRequest PutQuestionRequest = new("Is this a Question ?", "Yes", ["No", "Maybe", "?"],
            "Novice", "Antiquity", ["Literature", "Mangas"]);

    [Fact]
    public async Task ShouldReturn_200Ok_WhenIdExists()
    {
        //Arrange
        CreateQuestionRequest createRequest = new("Is this a Question ?", "Yes", ["No", "Maybe", "?"],
                "Novice", "Antiquity", ["Literature", "Mangas"]);
        var createResponse = await HttpClient.PostAsJsonAsync(BaseApiUrl, createRequest);
        var createContent = await createResponse.Content.ReadFromJsonAsync<QuestionResponse>();
        var createdQuestionId = createContent.id;
        var requestPath = new Uri(BaseApiUrl, createdQuestionId.ToString());

        //Act
        var response = await HttpClient.PutAsJsonAsync(requestPath, PutQuestionRequest);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task ShouldReturn_400BadRequest_WhenTitleIsEmpty()
    {
        //Arrange
        var id = Guid.NewGuid();
        var requestPath = new Uri(BaseApiUrl, id.ToString());
        var request = PutQuestionRequest with { title = "" };

        //Act
        var response = await HttpClient.PutAsJsonAsync(requestPath, request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturn_400BadRequest_WhenCorrectAnswerIsEmpty()
    {
        //Arrange
        var id = Guid.NewGuid();
        var requestPath = new Uri(BaseApiUrl, id.ToString());
        var request = PutQuestionRequest with { correctAnswer = "" };

        //Act
        var response = await HttpClient.PutAsJsonAsync(requestPath, request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturn_400BadRequest_WhenDifficultyIsEmpty()
    {
        //Arrange
        var id = Guid.NewGuid();
        var requestPath = new Uri(BaseApiUrl, id.ToString());
        var request = PutQuestionRequest with { difficulty = "" };

        //Act
        var response = await HttpClient.PutAsJsonAsync(requestPath, request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturn_400BadRequest_WhenThemesIsEmpty()
    {
        //Arrange
        var id = Guid.NewGuid();
        var requestPath = new Uri(BaseApiUrl, id.ToString());
        var request = PutQuestionRequest with { themes = [] };

        //Act
        var response = await HttpClient.PutAsJsonAsync(requestPath, request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturn_400BadRequest_WhenWrongAnswersIsEmpty()
    {
        //Arrange
        var id = Guid.NewGuid();
        var requestPath = new Uri(BaseApiUrl, id.ToString());
        var request = PutQuestionRequest with { wrongAnswers = [] };

        //Act
        var response = await HttpClient.PutAsJsonAsync(requestPath, request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturn_400BadRequest_WhenWrongAnswersContainsCorrectAnswer()
    {
        //Arrange
        var id = Guid.NewGuid();
        var requestPath = new Uri(BaseApiUrl, id.ToString());
        var request = PutQuestionRequest with { wrongAnswers = ["Answer1"], correctAnswer = "Answer1" };

        //Act
        var response = await HttpClient.PutAsJsonAsync(requestPath, request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
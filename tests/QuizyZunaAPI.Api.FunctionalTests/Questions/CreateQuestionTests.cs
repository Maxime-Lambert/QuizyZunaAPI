using System.Net;
using System.Net.Http.Json;

using QuizyZunaAPI.Application.Questions.CreateQuestion;
using QuizyZunaAPI.Application.Questions.Responses;

namespace QuizyZunaAPI.Api.FunctionalTests.Questions;

public class CreateQuestionTests(FunctionalTestWebAppFactory functionalTestWebAppFactory) : BaseFunctionalTest(functionalTestWebAppFactory)
{
    private static readonly CreateQuestionRequest CreateQuestionRequest = new("Is this a Question ?", "Yes", ["No", "Maybe", "?"],
            "Novice", "Antiquity", ["Literature", "Mangas"]);

    [Fact]
    public async Task ShouldReturn_201CreatedAt_WhenRequestIsCorrect()
    {
        //Arrange

        //Act
        var response = await HttpClient.PostAsJsonAsync(BaseApiUrl, CreateQuestionRequest);
        var content = await response.Content.ReadFromJsonAsync<QuestionResponse>();

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.First(header => header.Key == "Location").Value.First().Should().Be($"{BaseApiUrl}{content!.id}");
    }

    [Fact]
    public async Task ShouldReturn_400BadRequest_WhenTitleIsEmpty()
    {
        //Arrange
        var request = CreateQuestionRequest with { title = "" };

        //Act
        var response = await HttpClient.PostAsJsonAsync(BaseApiUrl, request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturn_400BadRequest_WhenCorrectAnswerIsEmpty()
    {
        //Arrange
        var request = CreateQuestionRequest with { correctAnswer = "" };

        //Act
        var response = await HttpClient.PostAsJsonAsync(BaseApiUrl, request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturn_400BadRequest_WhenDifficultyIsEmpty()
    {
        //Arrange
        var request = CreateQuestionRequest with { difficulty = "" };

        //Act
        var response = await HttpClient.PostAsJsonAsync(BaseApiUrl, request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturn_400BadRequest_WhenThemesIsEmpty()
    {
        //Arrange
        var request = CreateQuestionRequest with { themes = [] };

        //Act
        var response = await HttpClient.PostAsJsonAsync(BaseApiUrl, request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturn_400BadRequest_WhenWrongAnswersIsEmpty()
    {
        //Arrange
        var request = CreateQuestionRequest with { wrongAnswers = [] };

        //Act
        var response = await HttpClient.PostAsJsonAsync(BaseApiUrl, request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturn_400BadRequest_WhenWrongAnswersContainsCorrectAnswer()
    {
        //Arrange
        var request = CreateQuestionRequest with { wrongAnswers = ["Answer1"], correctAnswer = "Answer1" };

        //Act
        var response = await HttpClient.PostAsJsonAsync(BaseApiUrl, request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}

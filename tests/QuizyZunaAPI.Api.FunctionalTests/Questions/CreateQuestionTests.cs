using System.Net;
using System.Net.Http.Json;

using QuizyZunaAPI.Application.Questions.CreateQuestion;
using QuizyZunaAPI.Application.Questions.Responses;

namespace QuizyZunaAPI.Api.FunctionalTests.Questions;

public class CreateQuestionTests(FunctionalTestWebAppFactory functionalTestWebAppFactory) : BaseFunctionalTest(functionalTestWebAppFactory)
{
    private static readonly CreateQuestionRequest _createQuestionRequest = new("Is this a Question ?", "Yes", ["No", "Maybe", "?"],
            "Novice", "", ["Literature", "Mangas"]);

    [Fact]
    public async Task ShouldReturn_201CreatedAt_WhenRequestIsCorrect()
    {
        //Arrange

        //Act
        var response = await HttpClient.PostAsJsonAsync(BaseApiUrl, _createQuestionRequest);
        var content = await response.Content.ReadFromJsonAsync<QuestionResponse>();

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.First(header => header.Key == "Location").Value.First().Should().Be($"{BaseApiUrl}{content!.id}");
    }

    [Fact]
    public async Task ShouldReturn_400BadRequest_WhenTitleIsEmpty()
    {
        //Arrange
        var request = _createQuestionRequest with { title = "" };

        //Act
        var response = await HttpClient.PostAsJsonAsync(BaseApiUrl, request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturn_400BadRequest_WhenCorrectAnswerIsEmpty()
    {
        //Arrange
        var request = _createQuestionRequest with { correctAnswer = "" };

        //Act
        var response = await HttpClient.PostAsJsonAsync(BaseApiUrl, request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturn_400BadRequest_WhenDifficultyIsEmpty()
    {
        //Arrange
        var request = _createQuestionRequest with { difficulty = "" };

        //Act
        var response = await HttpClient.PostAsJsonAsync(BaseApiUrl, request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturn_400BadRequest_WhenThemesIsEmpty()
    {
        //Arrange
        var request = _createQuestionRequest with { themes = [] };

        //Act
        var response = await HttpClient.PostAsJsonAsync(BaseApiUrl, request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturn_400BadRequest_WhenWrongAnswersIsEmpty()
    {
        //Arrange
        var request = _createQuestionRequest with { wrongAnswers = [] };

        //Act
        var response = await HttpClient.PostAsJsonAsync(BaseApiUrl, request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturn_400BadRequest_WhenWrongAnswersContainsCorrectAnswer()
    {
        //Arrange
        var request = _createQuestionRequest with { wrongAnswers = ["Answer1"], correctAnswer = "Answer1" };

        //Act
        var response = await HttpClient.PostAsJsonAsync(BaseApiUrl, request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturn_400BadRequest_WhenYearHasWrongFormat()
    {
        //Arrange
        var request = _createQuestionRequest with { year = "2024" };

        //Act
        var response = await HttpClient.PostAsJsonAsync(BaseApiUrl, request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
